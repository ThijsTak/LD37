using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class HealthBar : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	void Update(){
		var text_component = this.GetComponent<UnityEngine.UI.Text> ();
		text_component.text = string.Format("Player energy: {0} of {1}", GlobalManager.Instance.player.GetEnergy (), GlobalManager.Instance.player.GetMaxEnegy ());
	}
}
