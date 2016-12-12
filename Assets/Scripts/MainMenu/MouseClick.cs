using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseClick : MonoBehaviour {

	public bool isStart;
	public bool isQuit;

	void OnMouseUp()
	{
		if (isStart)
		{
			SceneManager.LoadScene(1);
		}
		else if (isQuit)
		{
			Application.Quit();
		}
	}
}
