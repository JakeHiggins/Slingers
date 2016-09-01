using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public static class Settings
{
	public static float MusicVolume;
	public static float SFXVolume;
	public static int[] Resolution;
	public enum GraphicsQualityLevel { Low, Medium, High }
	public static GraphicsQualityLevel GraphicsQuality = GraphicsQualityLevel.High;

	/// <summary>
	/// Set Music Volume
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Success</returns>
	public static bool SetMusicVolume(float value)
	{
		if (value >= 0 && value <= 100)
		{
			MusicVolume = value;
			return true;
		}
		else
		{
			Debug.LogWarning("Invalid MusicVolume value, must be between 0 and 100.");
			return false;
		}
	}

	/// <summary>
	/// Set SFX Volume
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Success</returns>
	public static bool SetSFXVolume(float value)
	{
		if (value >= 0 && value <= 100)
		{
			SFXVolume = value;
			return true;
		}
		else
		{
			Debug.LogWarning("Invalid SFXVolume value, must be between 0 and 100.");
			return false;
		}
	}

	/// <summary>
	/// Set the resolution of the game
	/// </summary>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static bool SetResolution(int width, int height)
	{
		if(width > 0 || height > 0)
		{
			Resolution[0] = width;
			Resolution[1] = height;
			return true;
		}
		else
		{
			Debug.LogWarning("Invalid Resolution value(s), must be > 0.");
			return false;
		}
		
	}

	/// <summary>
	/// Set Quality of Graphics
	/// </summary>
	/// <param name="quality"></param>
	/// <returns></returns>
	public static bool SetGraphicsQuality(GraphicsQualityLevel quality)
	{
		return true;
	}

	public static void SaveSettings()
	{
		
	}

	public static void LoadSettings()
	{

	}
}
