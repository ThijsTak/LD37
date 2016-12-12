using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {
	public UnityEngine.UI.RawImage Target;
	public UnityEngine.UI.Button NextButton;
	public UnityEngine.UI.Button PrevButton;
	public List<IntroImage> IntroImages;

	private int index = 0;

	// Use this for initialization
	void Start () {
		
	}

	public void onNextImage(){
		if (index >= (IntroImages.Count - 1)) {
			LoadGameplayScene ();
			return;
		}

		index++;
	}
		
	public void onPrevImage (){
		if (index == 0) {
			LoadMainMenuScene ();
			return;
		}

		index--;
	}

	void LoadGameplayScene ()
	{
		throw new NotImplementedException ();
	}

	void LoadMainMenuScene ()
	{
		throw new NotImplementedException ();
	}
	
	// Update is called once per frame
	void Update () {
		if (0 == index) {
			PrevButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Main Menu";
		} else {
			PrevButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Previous";
		}

		if (index >= (IntroImages.Count - 1)) {
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