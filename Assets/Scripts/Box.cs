using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction //temp
{
	Left, Right
}

[RequireComponent(typeof(BoxCollider2D))]
public class Box : MonoBehaviour 
{
	[SerializeField]
	private BoxDirectionTrigger Left;
	[SerializeField]
	private BoxDirectionTrigger Right;
	[SerializeField]
	private BoxDirectionTrigger Up;

	public bool CanBeMoved(Direction direction)
	{
		if(Up.IsTriggered)
		{
			return false;
		}

		if(direction == Direction.Left)
		{
			return !Left.IsTriggered;
		}
		else
		{
			return !Right.IsTriggered;
		}
	}

	public bool CanClimb()
	{
		return !Up.IsTriggered;
	}

	public Box GetObject()
	{
		return this;
	}
}
