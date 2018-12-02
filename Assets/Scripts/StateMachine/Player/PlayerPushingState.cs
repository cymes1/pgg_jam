using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushingState : PlayerState
{
	private Direction direction;
	private GameObject target;
	private float destXPos;

	public PlayerPushingState(Player player)
		: base(player, StateID.PlayerPushingStateID)
	{

	}

	public override void DoBeforeEntering()
	{
		player.animator.SetBool("isPushing", true);
        player.ShiftBox();
	}

	public override void Act()
	{
		player.transform.Translate((int)direction * player.pushingSpeed * Time.deltaTime, 0, 0);
		if(destXPos * (int)direction < player.transform.position.x * (int)direction)
		{
			Vector3 newPos = player.transform.position;
			newPos.x = destXPos;
			player.transform.position = newPos;

			// box
			destXPos = player.transform.position.x + (int)direction * player.moveOffset;
			newPos = target.transform.position;
			newPos.x = destXPos;
			target.transform.position = newPos;

			ReturnToRegular();
		}
	}

	public override void DoBeforeLeaving()
	{
		player.animator.SetBool("isPushing", false);
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
			target = direction == Direction.LEFT ? player.LeftBox : player.RightBox;
		}
	}

	public delegate void PlayerReturnedToRegularEventHandler();
    public delegate void PlayerCrushedEventHandler();
	public event PlayerReturnedToRegularEventHandler PlayerReturnedToRegularEvent;
    public event PlayerCrushedEventHandler PlayerCrushedEvent;
}
