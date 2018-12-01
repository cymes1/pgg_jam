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

    bool moveXPlus = false;
    float startPositionX;
    

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
        }
        else if (transform.position.x <= startPositionX - rangeMove)
        {
            moveXPlus = true;
            staticBox.SetActive(true);
        }

        if (moveXPlus)
        {
            transform.Translate(speedCrane, 0, 0);
        }
        else
        {
            transform.Translate(-speedCrane, 0, 0);
        }
        if (testDrop)
        {
            InstantiateBox();
            testDrop = false;
            staticBox.SetActive(false);
        }
    }

    void InstantiateBox()
    {
        
        Instantiate(box, transform.position+respawnPosition, transform.rotation);
    }

}
