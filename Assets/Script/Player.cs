using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text steptext;
    [SerializeField, Range(.01f,1f)] float moveDuration = .2f;
    [SerializeField, Range(.01f,1f)] float jumpHeight = .4f;
    private float rightBorder;
    private float leftBorder;
    private float backBorder;
    [SerializeField] int maxTravel;
    [SerializeField] int currentTravel;
    public int MaxTravel { get => maxTravel;}
    public int CurrentTravel { get => currentTravel;}
    public bool IsDie {get => !this.enabled;}
    public AudioSource audioSource;
    public AudioClip jumpClip;

    public void SetUp(int backDistance,int extent){
        backBorder = backDistance - 1;
        leftBorder = -(extent + 1);
        rightBorder = extent + 1;
    }

    private void Update() {

        var moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            moveDir += new Vector3(0,0,1);

        if (Input.GetKey(KeyCode.DownArrow))
            moveDir += new Vector3(0,0,-1);

        if (Input.GetKey(KeyCode.RightArrow))
            moveDir += new Vector3(1,0,0);

        if (Input.GetKey(KeyCode.LeftArrow))
            moveDir += new Vector3(-1,0,0);

        if (moveDir != Vector3.zero && IsJumping() == false)
            Jump(moveDir);
    }

    private void Jump(Vector3 targetDirection) {
        Vector3 targetPosition = transform.position + targetDirection;
        transform.LookAt(targetPosition);

        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration/2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration/2));

        if (targetPosition.z <= backBorder || 
            targetPosition.x <= leftBorder || 
            targetPosition.x >= rightBorder)
            return;

        if (Tree.AllPositions.Contains(targetPosition))
                    return;
        
        audioSource.PlayOneShot(jumpClip);

        transform.DOMoveX(targetPosition.x, moveDuration);
        transform.DOMoveZ(targetPosition.z, moveDuration).OnComplete(UpdateTravel);
    }

    private void UpdateTravel() {
        currentTravel = (int) this.transform.position.z;
        if (currentTravel > maxTravel)
        maxTravel = currentTravel;

        steptext.text = "Score: " + (maxTravel*10).ToString();
    }

    public bool IsJumping() {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Car"){
            AnimateCrash();
        }
    }

    private void AnimateCrash()
    {
        transform.DOScaleY(.01f,1);
        transform.DOScaleX(2,1);
        this.enabled = false;
    }

}
