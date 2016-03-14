using UnityEngine;
using System.Collections;

public class InGameMasterController : UIController
{
	private UIController inGameHUDController;
	private UIController inGamePauseMenuController;
	private UIController inGameSettingsController;
	private UIController inGameQuitConfirmController;

	public override void Init()
	{
		base.Init();
		inGameHUDController = UIManager.Instance.GetUIController(UIManager.UIControllerID.InGameHUD);
		inGamePauseMenuController = UIManager.Instance.GetUIController(UIManager.UIControllerID.InGamePauseMenu);
		inGameSettingsController = UIManager.Instance.GetUIController(UIManager.UIControllerID.InGameSettings);
		inGameQuitConfirmController = UIManager.Instance.GetUIController(UIManager.UIControllerID.inGameQuitConfirm);
	}

	public override void Show()
	{
		base.Show();
		inGameHUDController.Show();
	}

	public override void Hide()
	{
		inGameHUDController.Hide();
		inGamePauseMenuController.Hide();
		inGameSettingsController.Hide();
		inGameQuitConfirmController.Hide();
		base.Hide();	
	}

	public void PauseGame()
	{
		//GameManager.Instance.PauseGame();
		GoToPauseGame();
	}

	public void UnPauseGame()
	{
		//GameManager.Instance.UnPauseGame();
		GoToInGame();
	}

	public void GoToQuitConfirm()
	{
		inGamePauseMenuController.Hide();
		inGameQuitConfirmController.Show();
	}

	public void GoToPauseGame()
	{
		inGameHUDController.Hide();
		inGamePauseMenuController.Show();
	}

	public void GoToInGame()
	{
		inGamePauseMenuController.Hide();
		inGameHUDController.Show();
	}

	public void QuitGame()
	{
		UIManager.Instance.PerformUIAction(UIManager.UIAction.GoToMainMenu);
	}
}
