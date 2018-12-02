using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegularState : PlayerState
{
	private bool isAxisInUse;
	private Rigidbody2D rigidbody;

	public PlayerRegularState(Player player)
		: base(player, StateID.PlayerRegularStateID)
	{
	}

	public override void DoBeforeEntering()
	{
		player.animator.SetBool("isIdle", true);
		rigidbody = player.GetComponent<Rigidbody2D>();
		rigidbody.bodyType = RigidbodyType2D.Dynamic;
	}

	public override void Act()
	{
		if(rigidbody.velocity.y != 0)
			return;

		if(Input.GetKeyDown(KeyCode.Z))
		{
			player.renderer.flipX = true;
			TakeClimbAction(Direction.LEFT);
		}
		else if(Input.GetKeyDown(KeyCode.X))
		{
			player.renderer.flipX = false;
			TakeClimbAction(Direction.RIGHT);
		}

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

	public override void DoBeforeLeaving()
	{
		player.animator.SetBool("isIdle", false);
		player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
	}

	private void TakeAction(Direction direction)
	{
		switch (direction)
		{
			case Direction.LEFT:
				player.renderer.flipX = true;
				if(player.LeftBox != null)
				{
					Box box = player.LeftBox.GetComponent<Box>();
					if(box == null)
						Move(Direction.LEFT);
					else if(box.CanBeMoved(Direction.LEFT))
						Push(Direction.LEFT);
					else
						return;
				}
				else
					Move(Direction.LEFT);
				break;

			default:
				player.renderer.flipX = false;
				if(player.RightBox != null)
				{
					Box box = player.RightBox.GetComponent<Box>();
					if(box == null)
						Move(Direction.RIGHT);
					else if(box.CanBeMoved(Direction.RIGHT))
						Push(Direction.RIGHT);
					else
						return;
				}
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
