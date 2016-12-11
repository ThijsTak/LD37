using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Helpers;
using Core;
using Units;
using UnityEngine;

namespace Units
{
	public class Enemy : BaseUnit
	{
		public Group Group;

		void Start()
		{
			transform.position = new Vector3(
				transform.position.x,
				HeightHelper.GetHeightFromTerrain(transform.position),
				transform.position.z);
		}
	}
}
