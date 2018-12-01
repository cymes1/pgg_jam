using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTunnel : MonoBehaviour {

    public GameObject[] respawnTunel;
    public Vector3 []respawnPosition;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            respawnPosition[i] = respawnTunel[i].transform.position;
        }
    }
}
 