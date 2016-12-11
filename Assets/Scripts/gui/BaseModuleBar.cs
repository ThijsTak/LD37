using System.Collections;
using System.Linq;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModuleBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var text_component = this.GetComponent<UnityEngine.UI.Text> ();
		text_component.text = string.Format("{0} out of {1} modules found", Core.GlobalManager.Instance.Home.GetShipComponentsState ().Count(x => x.IsExisting), Core.GlobalManager.Instance.Home.GetShipComponentsState ().Count());;
	}
}
