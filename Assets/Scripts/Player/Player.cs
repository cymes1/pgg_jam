using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movingSpeed;

    private PlayerStateMachine stateMachine;

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
        climbingState.AddTransition(Transition.PlayerCrushTransition, StateID.PlayerCrushedStateID);

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
        climbingState.PlayerCrushedEvent += stateMachine.OnPlayerCrush;
    }

    public PlayerStateMachine StateMachine { get { return stateMachine; } }
}