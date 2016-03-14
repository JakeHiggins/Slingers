using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	// Use this for initialization
	void Start()
	{
		if (Instance == null)
			Instance = this;

		UnityEngine.Object.DontDestroyOnLoad(gameObject.transform.root);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PauseGame()
	{

	}

	public void UnPauseGame()
	{

	}
}
