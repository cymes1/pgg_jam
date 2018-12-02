using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerState
{
	private Direction direction;
	private float destXPos;

	public PlayerMovingState(Player player)
		: base(player, StateID.PlayerMovingStateID)
	{}

	public override void DoBeforeEntering()
	{
		player.animator.SetBool("isWalking", true);
        player.WalkSoundOn();
	}

	public override void Act()
	{
		player.transform.Translate((int)direction * player.movingSpeed * Time.deltaTime, 0, 0);
		if(destXPos * (int)direction < player.transform.position.x * (int)direction)
		{
			Vector3 newPos = player.transform.position;
			newPos.x = destXPos;
			player.transform.position = newPos;
			ReturnToRegular();
		}
	}

	public override void DoBeforeLeaving()
	{
		player.animator.SetBool("isWalking", false);
        player.WalkSoundOff();
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
