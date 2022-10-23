using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFKSpawner : MonoBehaviour
{
    [SerializeField] GameObject ufoPrefab;
    [SerializeField] int spawnZPos = 5;
    [SerializeField] Player player;
    [SerializeField] float timeOut = 5;
    [SerializeField] float timer = 0;
    int playerLastMaxTravel = 0; 

    private void SpawnUFO(){
        player.enabled = false;
        var position = new Vector3(player.transform.position.x,1,player.CurrentTravel + spawnZPos);
        var rotation = Quaternion.Euler(0,180,0);
        var ufoObject = Instantiate(ufoPrefab, position, rotation);
        var ufo = ufoObject.GetComponent<Ufo>();
        ufo.SetUpTarget(player);
    }

    private void Update() {
        if (player.MaxTravel != playerLastMaxTravel){
            timer = 0;
            playerLastMaxTravel = player.MaxTravel;
            return;
        }

        if (timer <= timeOut){
            timer += Time.deltaTime;
            return;
        }

        if (player.IsJumping() == false && player.IsDie == false)
            SpawnUFO();
    }
}
