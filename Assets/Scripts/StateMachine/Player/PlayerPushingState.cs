﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushingState : PlayerState
{
	private Direction direction;
	private float destXPos;

	public PlayerPushingState(Player player)
		: base(player, StateID.PlayerPushingStateID)
	{

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
			destXPos = player.transform.position.x + (int)direction * player.moveOffset;
		}
	}

	public delegate void PlayerReturnedToRegularEventHandler();
    public delegate void PlayerCrushedEventHandler();
	public event PlayerReturnedToRegularEventHandler PlayerReturnedToRegularEvent;
    public event PlayerCrushedEventHandler PlayerCrushedEvent;
}
