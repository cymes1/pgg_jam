using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingState : PlayerState
{
	private Direction direction;

	private float xTarget;
	private float yTarget;

	public PlayerClimbingState(Player player)
		: base(player, StateID.PlayerClimbingStateID)
	{
	}

	public override void Act()
	{
		Vector3 position = player.transform.position;
		if(position.y < yTarget)
			position.y += player.climbSpeed * Time.deltaTime;
		else if(position.y > yTarget)
			position.y = yTarget;
		else if(position.y == yTarget)
		{
			if(position.x * (int)direction < xTarget * (int)direction)
				position.x += (int)direction * player.climbSpeed * Time.deltaTime;
			else if(position.x * (int)direction > xTarget * (int)direction)
				position.x = xTarget;
			else
				ReturnToRegular();
		}
		player.transform.position = position;
	}

	public void ReturnToRegular()
	{
		if(PlayerReturnedToRegularEvent != null)
			PlayerReturnedToRegularEvent();
	}

	public void Crush()
	{
		if(PlayerCrushedEvent != null)
			PlayerCrushedEvent();
	}

	public Direction Direction
	{
		set
		{
			direction = value;
			xTarget = player.transform.position.x + (int)direction * player.moveOffset;
			yTarget = player.transform.position.y + player.moveOffset;
		}
	}

	public delegate void PlayerReturnedToRegularEventHandler();
    public delegate void PlayerCrushedEventHandler();
	public event PlayerReturnedToRegularEventHandler PlayerReturnedToRegularEvent;
    public event PlayerCrushedEventHandler PlayerCrushedEvent;
}
