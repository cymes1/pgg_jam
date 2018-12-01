using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerState
{
	private Direction direction;
	private float currentXPos;
	private float offset = 1;
	private float epsilon = 0.3f;

	public PlayerMovingState(Player player)
		: base(player, StateID.PlayerMovingStateID)
	{}

	public override void DoBeforeEntering()
	{
		currentXPos = player.transform.position.x;
	}

	public override void Act()
	{
		player.transform.Translate((int)direction * player.movingSpeed * Time.deltaTime, 0, 0);
		if(Mathf.Abs((currentXPos + ((int)direction * offset)) - player.transform.position.x) < epsilon)
		{
			player.transform.position = Vector3.right * (currentXPos + ((int)direction * offset));
			ReturnToRegular();
		}
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

	public Direction Direction { set { direction = value; } }

	public delegate void PlayerReturnedToRegularEventHandler();
    public delegate void PlayerCrushedEventHandler();
	public event PlayerReturnedToRegularEventHandler PlayerReturnedToRegularEvent;
    public event PlayerCrushedEventHandler PlayerCrushedEvent;
}
