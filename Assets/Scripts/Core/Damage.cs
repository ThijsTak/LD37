using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	public class Damage : MonoBehaviour
	{
		public float Power;
		public PowerType Type;

		public enum PowerType
		{
			Normal = 0,
			Stun = 1
		}
	}
}
