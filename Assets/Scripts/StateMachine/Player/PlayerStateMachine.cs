using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
	public void OnPlayerPush()
	{
		PerformTransition(Transition.PlayerPushTransition);
	}

	public void OnPlayerMove(Direction dir)
	{
		PerformTransition(Transition.PlayerMoveTransition);
		((PlayerMovingState)currentState).Direction = dir;
	}
	
	public void OnPlayerClimb()
	{
		PerformTransition(Transition.PlayerClimbTransition);
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
