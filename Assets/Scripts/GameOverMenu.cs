using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class GameOverMenu : MonoBehaviour 
{
	[SerializeField] private GameObject defaultButton;
	private GameObject currentSelectedButton;
	[SerializeField] private AudioClip returnToMenuClip;
	[SerializeField] private AudioClip retryClip;
	private AudioSource audioSource;

	void Update()
	{
		if(currentSelectedButton != EventSystem.current.currentSelectedGameObject)
		{
			currentSelectedButton = EventSystem.current.currentSelectedGameObject;
			if(currentSelectedButton.name == "Retry")
			{
				audioSource.PlayOneShot(retryClip);
			}
			else
			{
				audioSource.PlayOneShot(returnToMenuClip);
			}
		}		
	}

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if(defaultButton != null)
		{
			EventSystem.current.SetSelectedGameObject(defaultButton);
			currentSelectedButton = EventSystem.current.currentSelectedGameObject;
			audioSource.PlayOneShot(retryClip);
		}
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void Retry()
	{
		SceneManager.LoadScene(1);
	}
}
