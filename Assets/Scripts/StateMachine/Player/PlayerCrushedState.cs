using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrushedState : PlayerState
{
	public PlayerCrushedState(Player player)
		: base(player, StateID.PlayerCrushedStateID)
	{

	}

	public override void DoBeforeEntering()
	{
		player.GetComponent<Collider2D>().isTrigger = true;
		player.gameOverScreen.SetActive(true);
	}
}
