using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;


namespace Core
{
	[Serializable]
	public class BasicSettings
	{
		[Header("Mule variables")]
		public float MuleFlyHeight = 10.0f;
		public float MuleDescentSpeed = 2.0f;
		public float MuleAscentSpeed = 2.0f;
		public float MuleDroppointDescentOffset = 1.5f;
		public float MuleSpeed = 30.0f;

		[Header("Player variables")]
		public float PlayerHoverHeight = 1.0f;
		public float PlayerRadarRange = 100.0f;
	}
}
