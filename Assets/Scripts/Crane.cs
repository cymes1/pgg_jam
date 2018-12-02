using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Crane : MonoBehaviour {

    public float rangeMove = 5f;
    public float speedCrane = 1f;
    public GameObject box;
    public bool testDrop = false;
    public Vector3 respawnPosition;
    public GameObject staticBox;
    public RespawnTunnel respawnTunel;
    public float y=-1f;
    public AudioClip dropBoxSound;

    bool moveXPlus = false;
    float startPositionX;
    bool isRespawnBox = false;
    int random;
    AudioSource audioSource;
    bool onlyOneRandomRound=true;
    int noMoreBoxes = 0;
    

	void Start ()
    {
        startPositionX = transform.position.x;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (transform.position.x >= startPositionX + rangeMove && noMoreBoxes <30)
        {
            moveXPlus = false;
            staticBox.SetActive(true);
            isRespawnBox = false;
            //random = Random.Range(0, 10);
            onlyOneRandomRound = true;
            LimitNumberBoxes2();
        }
        else if (transform.position.x <= startPositionX - rangeMove && noMoreBoxes < 30)
        {
            moveXPlus = true;
            staticBox.SetActive(true);
            isRespawnBox = false;
            //random = Random.Range(0, 10);
            onlyOneRandomRound = true;
            LimitNumberBoxes2();
        }

        if (moveXPlus)
        {
            transform.Translate(speedCrane, 0, 0);
        }
        else
        {
            transform.Translate(-speedCrane, 0, 0);
        }
        //Debug.Log(noMoreBoxes);
    }

    void InstantiateBox(int random)
    {
        respawnPosition = respawnTunel.respawnPosition[random];
        Instantiate(box, respawnPosition + new Vector3(0, y, 0), transform.rotation);
        audioSource.PlayOneShot(dropBoxSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isRespawnBox && collision.transform.position == respawnTunel.respawnTunel[random].transform.position)
        {
            InstantiateBox(random);
            noMoreBoxes++;
            respawnTunel.boxRespawnCounter[random]++;
            testDrop = false;
            staticBox.SetActive(false);
            isRespawnBox = true;
        }
    }
    void LimitNumberBoxes2()
    {
        if (onlyOneRandomRound && noMoreBoxes < 30)
        {
            do
            {
                random = Random.Range(0, 10);
                if (respawnTunel.boxRespawnCounter[random] <= 2)
                {
                    onlyOneRandomRound = false;
                }
            } while (respawnTunel.boxRespawnCounter[random] == 3);

        }
    }

    //void LimitNumberBoxes()
    //{
    //    if (onlyOneRandomRound)
    //    {
    //        if (respawnTunel.boxRespawnCounter[random] <= 1)
    //        {
    //            respawnTunel.boxRespawnCounter[random]++;
    //            onlyOneRandomRound = false;
    //        }
    //        else if (respawnTunel.boxRespawnCounter[random] >= 2 )
    //        {
    //            List<int> ints = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.OrderBy(e=>Random.value).ToList();
    //            while (ints.Count > 0)
    //            {
    //                if(respawnTunel.boxRespawnCounter[ints[0]] >= 2)
    //                {
    //                    ints.RemoveAt(0);
    //                    continue;
    //                }
    //                onlyOneRandomRound = false;
    //                random = ints[0];
    //                break;
    //            }
    //            /*
    //               for (int i = 0; i < 2; i++)
    //            {
    //                random = Random.Range(0, 10);
    //                if (respawnTunel.boxRespawnCounter[random] == 2)
    //                {
    //                    i--;
    //                    for (int j = 0; j< 10; j++)
    //                    {
    //                        if(respawnTunel.boxRespawnCounter[j]==2)
    //                        {
    //                            noMoreBoxes++;
    //                        }
    //                    }
    //                    if (noMoreBoxes == 10)
    //                    {
    //                        onlyOneRandomRound = false;
    //                        break;
    //                    }
    //                }
    //                else if (respawnTunel.boxRespawnCounter[random] <= 1)
    //                {
    //                    onlyOneRandomRound = false;
    //                    break;
    //                }
    //            }
    //        */
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}


}
