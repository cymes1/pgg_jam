using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
	public void OnPlayerPush(Direction dir)
	{
		PerformTransition(Transition.PlayerPushTransition);
		((PlayerPushingState)currentState).Direction = dir;
	}

	public void OnPlayerMove(Direction dir)
	{
		PerformTransition(Transition.PlayerMoveTransition);
		((PlayerMovingState)currentState).Direction = dir;
	}
	
	public void OnPlayerClimb(Direction dir)
	{
		PerformTransition(Transition.PlayerClimbTransition);
		((PlayerClimbingState)currentState).Direction = dir;
	}
	
	public void OnPlayerCrush()
	{
		PerformTransition(Transition.PlayerCrushTransition);
	}
	
	public void OnReturnToRegular()
	{
		PerformTransition(Transition.PlayerReturnToRegularTransition);
	}
}
