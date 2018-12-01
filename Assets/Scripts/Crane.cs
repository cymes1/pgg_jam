using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour {

    public float rangeMove = 5f;
    public float speedCrane = 1f;
    public GameObject box;
    public bool testDrop = false;
    public Vector3 respawnPosition;
    public GameObject staticBox;
    public RespawnTunnel respawnTunel;
    public float y=-1f;

    bool moveXPlus = false;
    float startPositionX;
    bool isRespawnBox = false;
    int random;
    

	void Start ()
    {
        startPositionX = transform.position.x;	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (transform.position.x >= startPositionX + rangeMove)
        {
            moveXPlus = false;
            staticBox.SetActive(true);
            isRespawnBox = false;
            random = Random.Range(0, 10);
        }
        else if (transform.position.x <= startPositionX - rangeMove)
        {
            moveXPlus = true;
            staticBox.SetActive(true);
            isRespawnBox = false;
            random = Random.Range(0, 10);
        }

        if (moveXPlus)
        {
            transform.Translate(speedCrane, 0, 0);
        }
        else
        {
            transform.Translate(-speedCrane, 0, 0);
        }

    }

    void InstantiateBox(int random)
    {
        
        respawnPosition = respawnTunel.respawnPosition[random];
        Instantiate(box, respawnPosition + new Vector3(0, y, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isRespawnBox && collision.transform.position == respawnTunel.respawnTunel[random].transform.position)
        {            
            InstantiateBox(random);
            testDrop = false;
            staticBox.SetActive(false);
            isRespawnBox = true;
        }
    }
    
    

}
