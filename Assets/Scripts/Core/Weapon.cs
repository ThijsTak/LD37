using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class Stunner
	{
		public float Power = 1;
		public float PowerPerShot = 0.25f;
		public Transform ShotPoint = null;
		public GameObject ProjectilePrefab = null;
	}
}
