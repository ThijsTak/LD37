using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Behaviours;
using Core;
using UnityEngine;

public class RadarView : MonoBehaviour
{
	public RadarCategory[] Categories;
	private List<RadarItem> _Items = new List<RadarItem>();
	const float RadarViewSize = 110; // The image is 128 and we have a border of a few pixels.

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Core.GlobalManager.Instance.player == null)
		{
			Debug.Log("Player is missing");
			return;
		}

		// Delete all previous elements
		foreach (var r in _Items)
		{
			// Remove after the duration of the current frame (can be a single frame but
			// never shorter then 1 frame at 120 fps and never longer then 1 frame at 20fps)
			DestroyObject(r.Pip, Mathf.Clamp(Time.time, 1.0f / 120.0f, 1.0f / 20.0f));
		}

		// Clean the list since everything is scheduled for delete anyway.
		_Items.Clear();


		var radarSource = Core.GlobalManager.Instance.player;
		var position = radarSource.transform.position;
		var rotation = radarSource.transform.rotation;

		foreach (var category in Categories.Where(x => x.IsEnabled).Reverse())
		{
			var radarRange = Mathf.Clamp(category.MaxDistance, 0, Core.GlobalManager.Instance.Settings.PlayerRadarRange);
			var tagged_objects = GameObject.FindGameObjectsWithTag(category.Name);
			foreach (var tagged_object in tagged_objects)
			{
				var offset = position - tagged_object.transform.position;

				var coll = tagged_object.GetComponent<Collectable>();
				bool alwaysShow = false;
				if (coll != null && StatCounter.Instance.SelecteDifficulty != StatCounter.Difficulty.Hard)
				{
					alwaysShow = coll.IsTagged;
				}

				if (offset.magnitude > radarRange && !category.IgnoreMaxDistance && !alwaysShow)
				{
					continue;
				}
				else if (offset.magnitude > radarRange)
				{
					offset = (offset / offset.magnitude) * Core.GlobalManager.Instance.Settings.PlayerRadarRange;
				}

				// Scale the offset to the radarRange


				var item = new RadarItem();

				// We need to rotate the offset based on the players current 
				offset = Quaternion.Inverse(rotation) * offset;
				var offset_nav = -new Vector3(offset.x, offset.z, 0);

				// Scale the image based on the radar view size
				offset_nav *= (RadarViewSize / Core.GlobalManager.Instance.Settings.PlayerRadarRange);

				var parent = this.gameObject.GetComponent<RectTransform>();
				var new_position = parent.position + offset_nav;
				if (category.RadarPip == null)
				{
					Debug.LogWarningFormat("The category \"{0}\" has no PIP model assigned.", category.Name);
				}

				item.Pip = Instantiate(category.RadarPip, new_position, Quaternion.identity, parent);
				_Items.Add(item);
			}
		}
	}

	/// <summary>
	/// A single item of a radar.
	/// </summary>
	public class RadarItem
	{
		public GameObject Pip { get; set; }
	}
}

