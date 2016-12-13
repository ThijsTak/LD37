using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseBehaviour : MonoBehaviour
{

	public ButtonType type;

	public enum ButtonType
	{
		Static = 0,
		Start = 1,
		Quit = 2,
		Main = 3,
		Statistics = 4,
		Easy = 5,
		Medium = 6,
		Hard = 7
	}

	void Start()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}

	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.green;
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}

	void OnMouseUp()
	{
		switch (type)
		{
			case ButtonType.Static:
				return;
			case ButtonType.Start:
				SceneManager.LoadScene(SceneHelper.Intro);
				return;
			case ButtonType.Quit:
				Application.Quit();
				return;
			case ButtonType.Main:
				SceneManager.LoadScene(SceneHelper.MainMenu);
				break;
			case ButtonType.Statistics:
				SceneManager.LoadScene(SceneHelper.Statistics);
				break;
			case ButtonType.Easy:
				StatCounter.Instance.SelecteDifficulty = StatCounter.Difficulty.Easy;
				SceneManager.LoadScene(SceneHelper.Intro);
				break;
			case ButtonType.Medium:
				StatCounter.Instance.SelecteDifficulty = StatCounter.Difficulty.Medium;
				SceneManager.LoadScene(SceneHelper.Intro);
				break;
			case ButtonType.Hard:
				StatCounter.Instance.SelecteDifficulty = StatCounter.Difficulty.Hard;
				SceneManager.LoadScene(SceneHelper.Intro);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
