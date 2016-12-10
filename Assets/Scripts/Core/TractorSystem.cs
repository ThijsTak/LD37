using System;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class TractorSystem
	{
		[SerializeField]
		public float Power = 1.0f;

		[SerializeField]
		public float Radius = 2.5f;

		[SerializeField]
		public Transform Target = null;

		[SerializeField]
		public bool Active = false;

		public Rigidbody TracRigidbody = null;
	}
}
