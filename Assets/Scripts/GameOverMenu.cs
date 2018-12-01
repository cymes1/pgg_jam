using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour 
{
	public void ReturnToMainMenu()
	{

	}

	public void Retry()
	{
		SceneManager.LoadScene(0);
	}
}
