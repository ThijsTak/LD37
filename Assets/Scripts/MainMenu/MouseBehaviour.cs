using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseBehaviour : MonoBehaviour {

	public bool isStart;
	public bool isQuit;

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
		if (isStart)
		{
			SceneManager.LoadScene(SceneHelper.Intro);
		}
		else if (isQuit)
		{
			Application.Quit();
		}
	}
}
