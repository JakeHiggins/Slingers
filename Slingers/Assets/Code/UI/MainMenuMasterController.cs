using UnityEngine;
using System.Collections;

public class MainMenuMasterController : UIController
{
	private UIController mainMenuSettingsController;
	private UIController mainMenuMainController;

	public override void Init()
	{
		base.Init();
		mainMenuSettingsController = UIManager.Instance.GetUIController(UIManager.UIControllerID.MainMenuSettings);
		mainMenuMainController = UIManager.Instance.GetUIController(UIManager.UIControllerID.MainMenuMain);
	}

	public override void Show()
	{
		base.Show();
		mainMenuMainController.Show();
	}

	public override void Hide()
	{
		base.Hide();
		mainMenuMainController.Hide();
		mainMenuSettingsController.Hide();
	}

	public void GoToMainMenuSettings()
	{
		mainMenuMainController.Hide();
		mainMenuSettingsController.Show();
	}

	public void GoToMainMenuMain()
	{
		mainMenuSettingsController.Hide();
		mainMenuMainController.Show();
	}

	public void GoToInGame()
	{
		UIManager.Instance.PerformUIAction(UIManager.UIAction.GoToInGame);
	}
}
