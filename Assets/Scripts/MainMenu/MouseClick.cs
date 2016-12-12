using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseClick : MonoBehaviour {

	public bool isStart;
	public bool isQuit;

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
