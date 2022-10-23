using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBlock : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject repet;
    private int extent;
    public int Extent { get => extent; }
    public void Build (int extent){

        this.extent = extent;

        for (int i = -1; i <= 1; i++)
        {
            if (i == 0)
                continue;

            var m = Instantiate(main);
            m.transform.SetParent(this.transform);
            m.transform.localPosition = new Vector3(-(extent+1)*i,0,0);
            m.transform.GetComponentInChildren<Renderer>().material.color *= Color.gray;
        }

        main.transform.localScale = new Vector3(
            x: extent * 2 + 1,
            y: main.transform.localScale.y,
            z: main.transform.localScale.z
        );

        if(repet == null)
            return;

        for (int x = -(extent+1); x <= extent+1; x++){
            if(x==0)
                continue;

            var r = Instantiate(repet);
            r.transform.SetParent(this.transform);
            r.transform.localPosition = new Vector3(x,0,0);
        }
    }
}
