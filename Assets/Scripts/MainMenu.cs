using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour 
{
	[SerializeField] private GameObject defaultButton;
	private GameObject currentSelectedButton;
	private AudioSource audioSource;

	[SerializeField] private AudioClip startClip;
	[SerializeField] private AudioClip creditsClip;
	[SerializeField] private AudioClip settingsClip;
	[SerializeField] private AudioClip exitClip;


	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
		if(defaultButton != null)
		{
			currentSelectedButton =	defaultButton;
			EventSystem.current.SetSelectedGameObject(defaultButton);
			audioSource.PlayOneShot(startClip);
		}
	}

	void Update () 
	{
		if(EventSystem.current.currentSelectedGameObject != currentSelectedButton)
		{
			currentSelectedButton = EventSystem.current.currentSelectedGameObject;
			switch(currentSelectedButton.name)
			{
				case "Start":
					audioSource.PlayOneShot(startClip);
					break;
				case "Credits":
					audioSource.PlayOneShot(creditsClip);
					break;
				case "Settings":
					audioSource.PlayOneShot(settingsClip);
					break;
				case "Exit":
					audioSource.PlayOneShot(exitClip);
					break;
			}
		}
	}

	public void OnStart()
	{
		SceneManager.LoadScene(1);
	}

	public void OnCredits()
	{
		//todo
	}

	public void OnSettings()
	{
		//todo
	}

	public void OnExit()
	{
		Application.Quit();
	}
}
