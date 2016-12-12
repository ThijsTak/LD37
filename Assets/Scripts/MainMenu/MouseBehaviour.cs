using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseBehaviour : MonoBehaviour {

	public ButtonType type;

	public enum ButtonType
	{
		Static = 0,
		Start = 1,
		Quit = 2,
		Main = 3
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
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
