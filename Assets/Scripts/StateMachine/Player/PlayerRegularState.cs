using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegularState : PlayerState
{
	private bool isAxisInUse;

	public PlayerRegularState(Player player)
		: base(player, StateID.PlayerRegularStateID)
	{
	}

	public override void Act()
	{
		if(Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f)
		{
			isAxisInUse = false;
			return;
		}
		else if(!isAxisInUse)
		{
			isAxisInUse = true;
			Move(Input.GetAxis("Horizontal") > 0 ? Direction.RIGHT : Direction.LEFT);
		}
	}

	public void Move(Direction dir)
	{
		if(PlayerStartedMovingEvent != null)
			PlayerStartedMovingEvent(dir);
	}

	public void Push()
	{
		if(PlayerStartedPushingEvent != null)
			PlayerStartedPushingEvent();
	}

	public void Climb()
	{
		if(PlayerStartedClimbingEvent != null)
			PlayerStartedClimbingEvent();
	}

	public void Crush()
	{
		if(PlayerCrushedEvent != null)
			PlayerCrushedEvent();
	}

	public delegate void PlayerStartedMovingEventHandler(Direction dir);
    public delegate void PlayerStartedPushingEventHandler();
    public delegate void PlayerStartedClimbingEventHandler();
    public delegate void PlayerCrushedEventHandler();
    public event PlayerStartedMovingEventHandler PlayerStartedMovingEvent;
    public event PlayerStartedPushingEventHandler PlayerStartedPushingEvent;
    public event PlayerStartedClimbingEventHandler PlayerStartedClimbingEvent;
    public event PlayerCrushedEventHandler PlayerCrushedEvent;
}
