using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movingSpeed;
    public float pushingSpeed;
    public float moveOffset = 1.5f;
    public float climbSpeed = 1;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public AudioClip walk, climb, reflectLadder,shiftBox;
    public AudioSource audioSource;

    private PlayerStateMachine stateMachine;
    private GameObject leftBox;
    private GameObject rightBox;
    private bool isOnGround;

    void Start()
    {
        InitStateMachine();
    }

    void Update()
    {
        stateMachine.CurrentState.Act();       
    }

    private void InitStateMachine()
    {
        PlayerRegularState regularState = new PlayerRegularState(this);
        regularState.AddTransition(Transition.PlayerMoveTransition, StateID.PlayerMovingStateID);
        regularState.AddTransition(Transition.PlayerPushTransition, StateID.PlayerPushingStateID);
        regularState.AddTransition(Transition.PlayerClimbTransition, StateID.PlayerClimbingStateID);
        regularState.AddTransition(Transition.PlayerCrushTransition, StateID.PlayerCrushedStateID);

        PlayerMovingState movingState = new PlayerMovingState(this);
        movingState.AddTransition(Transition.PlayerReturnToRegularTransition, StateID.PlayerRegularStateID);
        movingState.AddTransition(Transition.PlayerCrushTransition, StateID.PlayerCrushedStateID);

        PlayerPushingState pushingState = new PlayerPushingState(this);
        pushingState.AddTransition(Transition.PlayerReturnToRegularTransition, StateID.PlayerRegularStateID);
        pushingState.AddTransition(Transition.PlayerCrushTransition, StateID.PlayerCrushedStateID);

        PlayerClimbingState climbingState = new PlayerClimbingState(this);
        climbingState.AddTransition(Transition.PlayerReturnToRegularTransition, StateID.PlayerRegularStateID);
        //climbingState.AddTransition(Transition.PlayerCrushTransition, StateID.PlayerCrushedStateID);

        PlayerCrushedState crushedState = new PlayerCrushedState(this);

        stateMachine = new PlayerStateMachine();
        stateMachine.AddState(regularState);
        stateMachine.AddState(movingState);
        stateMachine.AddState(pushingState);
        stateMachine.AddState(climbingState);
        stateMachine.AddState(crushedState);

        regularState.PlayerStartedMovingEvent += stateMachine.OnPlayerMove;
        regularState.PlayerStartedPushingEvent += stateMachine.OnPlayerPush;
        regularState.PlayerStartedClimbingEvent += stateMachine.OnPlayerClimb;
        regularState.PlayerCrushedEvent += stateMachine.OnPlayerCrush;

        movingState.PlayerReturnedToRegularEvent += stateMachine.OnReturnToRegular;
        movingState.PlayerCrushedEvent += stateMachine.OnPlayerCrush;

        pushingState.PlayerReturnedToRegularEvent += stateMachine.OnReturnToRegular;
        pushingState.PlayerCrushedEvent += stateMachine.OnPlayerCrush;

        climbingState.PlayerReturnedToRegularEvent += stateMachine.OnReturnToRegular;
        //climbingState.PlayerCrushedEvent += stateMachine.OnPlayerCrush;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Trigger")       
        {
            GameObject otherObject = other.transform.parent.gameObject;
            //if(Mathf.Abs(otherObject.transform.position.y - transform.position.y) > 0.2f)
            //    return;

            if(otherObject.transform.position.x < transform.position.x)
            {
                leftBox = otherObject;
            }
            else if(otherObject.transform.position.x > transform.position.x)
            {
                rightBox = otherObject;
            }
        }
        else if(other.tag == "TriggerDown")
        {
            stateMachine.OnPlayerCrush();
        }
        else if(other.tag == "TriggerUp")
        {
            isOnGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Trigger")
        {
            GameObject otherObject = other.transform.parent.gameObject;

            if(otherObject.Equals(leftBox))
            {
                leftBox = null;
            }
            else if(otherObject.Equals(rightBox))
            {
                rightBox = null;
            }
        }
        if(other.tag == "TriggerUp")
        {
            isOnGround = false;
        }
    }

    public void WalkSoundOn()
    {
        audioSource.clip = walk;
        audioSource.pitch = 2;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void WalkSoundOff()
    {       
        audioSource.loop = false;
        audioSource.pitch = 1;
        audioSource.Stop();
        audioSource.PlayOneShot(walk);
    }

    public void ClimbSoundOn()
    {
        audioSource.clip = climb;
        audioSource.pitch = 4;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ClimbSoundOff()
    {
        audioSource.loop = false;
        audioSource.pitch = 1;
        audioSource.Stop();
    }
    public void ReflectLadder()
    {
        audioSource.PlayOneShot(reflectLadder);
    }

    public void ShiftBox()
    {
        audioSource.PlayOneShot(shiftBox);
    }

    public PlayerStateMachine StateMachine { get { return stateMachine; } }
    public GameObject LeftBox
    {
        get { return leftBox;  }
        set { leftBox = value; }
    }
    public GameObject RightBox
    {
        get { return rightBox;  }
        set { rightBox = value; }
    }
    public bool IsOnGround { get { return isOnGround; } }
}