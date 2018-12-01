using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxDirectionTrigger : MonoBehaviour 
{
	private bool isTriggered = false;

	[HideInInspector]
	public bool IsTriggered { get {return isTriggered;}}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Trigger")
		{
			isTriggered = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Trigger")
		{
			isTriggered = false;
		}
	}
}
