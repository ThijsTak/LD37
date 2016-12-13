using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

[Serializable]	
public class RadarCategory{
	public string Name;

	public GameObject RadarPip;

	/// <summary>
	/// The maximum distance the radar can see things.
	/// </summary>
	public float MaxDistance = 100.0f;

	/// <summary>
	/// The maximum distance will be ignored.
	/// </summary>
	public bool IgnoreMaxDistance = false;

	/// <summary>
	/// Will this pip be shown on the radar?
	/// </summary>
	public bool IsEnabled = true;
}
