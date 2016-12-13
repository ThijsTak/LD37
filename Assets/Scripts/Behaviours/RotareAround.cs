using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class RotareAround : MonoBehaviour
	{
		public Transform Parent;
		public Vector3 RotateDirection;
		public float Speed = 15.0f;

		public void Update()
		{
			transform.RotateAround(Parent.position, RotateDirection, Speed * Time.deltaTime);
		}
	}
}
