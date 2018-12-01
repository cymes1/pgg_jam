using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
	protected Player player;

	public PlayerState(Player player, StateID id)
	{
		this.player = player;
		this.stateID = id;
	}
}
