using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingState : PlayerState
{
	public PlayerClimbingState(Player player)
		: base(player, StateID.PlayerClimbingStateID)
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

	public delegate void PlayerReturnedToRegularEventHandler();
    public delegate void PlayerCrushedEventHandler();
	public event PlayerReturnedToRegularEventHandler PlayerReturnedToRegularEvent;
    public event PlayerCrushedEventHandler PlayerCrushedEvent;
}
