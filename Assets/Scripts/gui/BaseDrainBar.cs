using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class BaseDrainBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var text_component = this.GetComponent<UnityEngine.UI.Text> ();
		text_component.text = string.Format("{0:n2} energy per minute", -GlobalManager.Instance.Home.totalEnergyDrainPerMinute);
	}
}
