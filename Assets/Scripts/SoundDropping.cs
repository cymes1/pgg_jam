using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDropping : MonoBehaviour {

    public GameObject player;
    public AudioSource audioSource;
    public AudioClip groundHit;

    Vector3 playerPos;
    Vector3 boxPosition;
    float distance;
    bool pitchUp1 = true;
    bool pitchUp2 = true;
    bool pitchUp3 = true;
    bool gruondHitOneShotSounds = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update ()
    {
        playerPos = player.transform.position;
        boxPosition = transform.position;
        distance = boxPosition.y - playerPos.y;
        if (distance < 5.5f && pitchUp1)
        {
            audioSource.pitch = 1f;
           
            if (distance < 3.75f)
            {
                pitchUp1 = false;
            }
        }
        else if (distance < 3.75f && pitchUp2)
        {
            audioSource.pitch = 1.5f;
            if (distance < 2.85f)
            {
                pitchUp2 = false;
            }
        }
        else if(distance < 2.85f && pitchUp3)
        {
            audioSource.pitch = 2.5f;
            pitchUp3 = false;
        }
        if(distance < 1.51f)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;            
            //audioSource.enabled = false;
            if(distance<0.5f && gruondHitOneShotSounds)
            {
                audioSource.priority = 50;
                audioSource.PlayOneShot(groundHit);
                gruondHitOneShotSounds = false;
            }
        }  
        
        //Debug.Log(distance);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name =="Up")
        {
            //audioSource.playOnAwake = false;
            //audioSource.loop = false;
            audioSource.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name=="Up")
        {
            //audioSource.playOnAwake = true;
            //audioSource.loop = true; 
            audioSource.enabled = false;
        }
    }



}
