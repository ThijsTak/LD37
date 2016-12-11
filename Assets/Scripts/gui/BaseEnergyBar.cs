using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnergyBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var text_component = this.GetComponent<UnityEngine.UI.Text> ();
		text_component.text = string.Format("Base Energy: {0} of {1}", Core.GlobalManager.Instance.Home.GetEnergy (), Core.GlobalManager.Instance.Home.GetMaxEnegy ());
	}
}
