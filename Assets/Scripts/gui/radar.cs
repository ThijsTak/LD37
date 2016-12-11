using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public class radar : MonoBehaviour {

	public RadarCategory[] Categories;

	public Transform RadarSource;

	private List<RadarItem> _Items = new List<RadarItem> ();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Delete all previous elements
		foreach (var r in _Items) {
			DestroyObject (r.Pip, Time.deltaTime );
		}

		_Items.Clear ();


		var position = RadarSource.position;
		var rotation = RadarSource.rotation;

		foreach (var category in Categories) {
			var tagged_objects = GameObject.FindGameObjectsWithTag (category.Name);
			foreach (var tagged_object in tagged_objects) {
				var offset = position-tagged_object.transform.position;

				offset = RadarSource.TransformVector (-offset);

				// Debug.LogFormat ("Distance: {0}", offset.magnitude);
				if (offset.magnitude > 100) {
					
					continue;
				}

				var item = new RadarItem ();

				var parent = this.gameObject.GetComponent<RectTransform> ();
				var x_offset = Vector3.Project (offset, Vector3.right);
				// var y_offset = Vector3.Project(offset, Vector3.up);
				var z_offset = Vector3.Project(offset, Vector3.back);
				var offset_nav =  new Vector3(z_offset.z, x_offset.x, 0);
				var new_position = parent.position + offset_nav;
				item.Pip = Instantiate (category.RadarPip, new_position, Quaternion.identity, parent);

				_Items.Add (item);
			}
		}
	}

	public class RadarItem {
		public int Id {get;set;}
		public GameObject Cause { get; set; }
		public GameObject Pip { get; set; }
	}
}

[Serializable]	
public struct RadarCategory{
	[SerializeField]
	public string Name;

	[SerializeField]
	public GameObject RadarPip;
}
