using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour 
{
	[SerializeField] private GameObject defaultButton;
	[SerializeField] private GameObject backButton;
	private GameObject currentSelectedButton;
	private AudioSource audioSource;
	[SerializeField] private GameObject credits;

	[SerializeField] private AudioClip startClip;
	[SerializeField] private AudioClip creditsClip;
	[SerializeField] private AudioClip exitClip;
	[SerializeField] private AudioClip backClip;


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
		credits.SetActive(true);
		EventSystem.current.SetSelectedGameObject(backButton);
		audioSource.PlayOneShot(backClip);
	}

	public void OnBack()
	{
		credits.SetActive(false);
		EventSystem.current.SetSelectedGameObject(defaultButton);
		audioSource.PlayOneShot(startClip);
	}

	public void OnExit()
	{
		Application.Quit();
	}
}
