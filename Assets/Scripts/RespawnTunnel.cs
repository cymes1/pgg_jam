using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTunnel : MonoBehaviour {

    public GameObject[] respawnTunel;
    public Vector3 []respawnPosition;
    public int[] boxRespawnCounter;
    

    private void Start()
    {
        boxRespawnCounter = new int[10];
        for (int i = 0; i < 10; i++)
        {
            respawnPosition[i] = respawnTunel[i].transform.position;
        }
    }
}
 