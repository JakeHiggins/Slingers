using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class ApplicationManager
{
	public static void GoToInGame()
	{
		SceneManager.LoadScene("Prototype");
	}

	public static void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

}
