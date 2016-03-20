using UnityEngine;
using System.Collections;

public class MainMenuSettingsController : UIController
{
	public override void Init()
	{
		base.Init();
	}

	public override void Show()
	{
		base.Show();
	}

	public override void Hide()
	{
		base.Hide();
	}

	public void SetMusicVolume(float value)
	{
		Settings.SetMusicVolume(value);
	}

	public void SetSFXVolume(float value)
	{
		Settings.SetSFXVolume(value);
	}

	public void SetResolution(int width, int height)
	{
		Settings.SetResolution(width, height);
	}

	public void SetGraphicsQuality(Settings.GraphicsQualityLevel quality)
	{
		Settings.SetGraphicsQuality(quality);
	}
}
