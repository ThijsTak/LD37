﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RadarView : MonoBehaviour {
	public Transform RadarSource;
	public float RadarRange = 100;
	public RadarCategory[] Categories;


	private List<RadarItem> _Items = new List<RadarItem> ();
	const float RadarViewSize = 110; // The image is 128 and we have a border of a few pixels.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (RadarSource == null) {
			Debug.Log ("RadarSource is missing");
			return;
		}

		// Delete all previous elements
		foreach (var r in _Items) {
			// Remove after the duration of the current frame (can be a single frame but
			// never shorter then 1 frame at 120 fps and never longer then 1 frame at 20fps)
			DestroyObject (r.Pip, Mathf.Clamp(Time.time, 1.0f/120, 1.0f/20));
		}

		// Clean the list since everything is scheduled for delete anyway.
		_Items.Clear ();

		var position = RadarSource.position;
		var rotation = RadarSource.rotation;

		foreach (var category in Categories) {
			var tagged_objects = GameObject.FindGameObjectsWithTag (category.Name);
			foreach (var tagged_object in tagged_objects) {
				var offset = position - tagged_object.transform.position;

				if (offset.magnitude > RadarRange) {
					continue;
				}

				var item = new RadarItem ();

				// We need to rotate the offset based on the players current 
				offset = Quaternion.Inverse(rotation) * offset;
				var offset_nav =  -new Vector3(offset.x, offset.z, 0);

				// Scale the image based on the radar view size
				offset_nav *= (RadarViewSize / RadarRange); 

				var parent = this.gameObject.GetComponent<RectTransform> ();
				var new_position = parent.position + offset_nav;
				if (category.RadarPip == null) {
					Debug.LogWarningFormat ("The category \"{0}\" has no PIP model assigned.", category.Name);
				}

				item.Pip = Instantiate (category.RadarPip, new_position, Quaternion.identity, parent);
				_Items.Add (item);
			}
		}
	}

	/// <summary>
	/// A single item of a radar.
	/// </summary>
	public class RadarItem {
		public GameObject Pip { get; set; }
	}
}

