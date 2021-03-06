﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Helpers;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
	public UnityEngine.UI.RawImage Target;
	public UnityEngine.UI.Button NextButton;
	public UnityEngine.UI.Button PrevButton;
	public List<IntroImage> IntroImages;

	private int index = 0;

	// Use this for initialization
	void Start () {
		
	}

	private bool CanSkip() {
		return Input.GetButton ("Boost");
	}

	public void onNextImage(){
		if (index >= (IntroImages.Count - 1) || CanSkip()) {
			LoadGameplayScene ();
			return;
		}

		index++;
	}
		
	public void onPrevImage (){
		if (index == 0|| CanSkip()) {
			LoadMainMenuScene ();
			return;
		}

		index--;
	}

	public void LoadGameplayScene ()
	{
		SceneManager.LoadScene(SceneHelper.Game);
	}

	void LoadMainMenuScene ()
	{
		SceneManager.LoadScene(SceneHelper.MainMenu);
	}
	
	// Update is called once per frame
	void Update () {
		if (0 == index || CanSkip()) {
			PrevButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Main Menu";
		} else {
			PrevButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Previous";
		}

		if (index >= (IntroImages.Count - 1) || CanSkip()) {
			NextButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Start";
		} else {
			NextButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Next";
		}
		Target.texture = IntroImages [index].Image;

	}
}


[Serializable]
public class IntroImage {
	public string Name;
	public Texture Image;
}