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
		if(Input.GetKeyDown(KeyCode.Z))
			TakeClimbAction(Direction.LEFT);
		else if(Input.GetKeyDown(KeyCode.X))
			TakeClimbAction(Direction.RIGHT);

		if(Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f)
		{
			isAxisInUse = false;
			return;
		}
		else if(!isAxisInUse)
		{
			isAxisInUse = true;
			TakeAction(Input.GetAxis("Horizontal") > 0 ? Direction.RIGHT : Direction.LEFT);
		}
	}

	private void TakeAction(Direction direction)
	{
		switch (direction)
		{
			case Direction.LEFT:
				if(player.LeftBox != null)
					if(player.LeftBox.GetComponent<Box>().CanBeMoved(Direction.LEFT))
						Push(Direction.LEFT);
					else
						return;
				else
					Move(Direction.LEFT);
				break;

			default:
				if(player.RightBox != null)
					if(player.RightBox.GetComponent<Box>().CanBeMoved(Direction.RIGHT))
						Push(Direction.RIGHT);
					else
						return;
				else
					Move(Direction.RIGHT);
				break;
		}
	}

	private void TakeClimbAction(Direction direction)
	{
		switch (direction)
		{
			case Direction.LEFT:
				if(player.LeftBox != null && player.LeftBox.GetComponent<Box>().CanClimb())
					Climb(Direction.LEFT);
				break;

			default:
				if(player.RightBox != null && player.RightBox.GetComponent<Box>().CanClimb())
					Climb(Direction.RIGHT);
				break;
		}
	}

	public void Move(Direction dir)
	{
		if(PlayerStartedMovingEvent != null)
			PlayerStartedMovingEvent(dir);
	}

	public void Push(Direction dir)
	{
		if(PlayerStartedPushingEvent != null)
			PlayerStartedPushingEvent(dir);
	}

	public void Climb(Direction dir)
	{
		if(PlayerStartedClimbingEvent != null)
		{
			PlayerStartedClimbingEvent(dir);
		}
	}

	public void Crush()
	{
		if(PlayerCrushedEvent != null)
			PlayerCrushedEvent();
	}

	public delegate void PlayerStartedMovingEventHandler(Direction dir);
    public delegate void PlayerStartedPushingEventHandler(Direction dir);
    public delegate void PlayerStartedClimbingEventHandler(Direction dir);
    public delegate void PlayerCrushedEventHandler();
    public event PlayerStartedMovingEventHandler PlayerStartedMovingEvent;
    public event PlayerStartedPushingEventHandler PlayerStartedPushingEvent;
    public event PlayerStartedClimbingEventHandler PlayerStartedClimbingEvent;
    public event PlayerCrushedEventHandler PlayerCrushedEvent;
}
