using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

	public enum UIAction
	{
		GoToSplash,
		GoToGameLoad,
		GoToMainMenu,
		GoToInGame,
		GoToInGameSettings,
		PauseGame,
		UnPauseGame,
	}

	public enum UIControllerID : int
	{
		None = 0,
		Splash = 1,
		Loading = 2,
		MainMenuMaster = 3,
		MainMenuMain = 4,
		MainMenuSettings = 5,
		InGameMaster = 6,
		InGameHUD = 7,
		InGamePauseMenu = 8,
		InGameSettings = 9,
		inGameQuitConfirm = 10,
	}

	public static UIManager Instance { get; private set; }

	[SerializeField]
	private List<UIController> _controllers = new List<UIController>();

	private void Start()
	{
		if (Instance == null)
			Instance = this;

		UnityEngine.Object.DontDestroyOnLoad(gameObject.transform.root);
		var controllers = transform.GetComponentsInChildren<UIController>(true);
		//get all controllers first
		foreach (var item in controllers)
		{
			_controllers.Add(item);
		}
		//then initialize
		foreach (var item in controllers)
		{
			item.Init();
		}
	}

	public List<UIController> GetUIControllers()
	{
		return _controllers;
	}

	public void PerformUIAction(UIAction action)
	{
		switch(action)
		{
			case UIAction.GoToSplash:

				break;
			case UIAction.GoToGameLoad:

				break;
			case UIAction.GoToMainMenu:
				ApplicationManager.GoToMainMenu();
				GetUIController(UIControllerID.MainMenuMaster).Show();
				GetUIController(UIControllerID.InGameMaster).Hide();
				break;
			case UIAction.GoToInGame:
				ApplicationManager.GoToInGame();
				GetUIController(UIControllerID.MainMenuMaster).Hide();
				GetUIController(UIControllerID.InGameMaster).Show();
				break;
			case UIAction.PauseGame:

				break;
			case UIAction.UnPauseGame:

				break;
		}
	}

	public UIController GetUIController(UIControllerID id)
	{
		foreach(UIController controller in _controllers)
		{
			if (controller.ID == id) return controller;
		}
		return null;
	}
}
