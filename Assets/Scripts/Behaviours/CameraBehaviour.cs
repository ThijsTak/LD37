using System;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class CameraBehaviour : MonoBehaviour
	{
		public Transform Target;
		public BehaviourType Behaviour;

		public float zoom = 50;

		public enum BehaviourType
		{
			FixedPoint
		}

		public void Start()
		{

		}

		public void Update()
		{
			switch (Behaviour)
			{
				case BehaviourType.FixedPoint:
					FixedBehaviour();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void FixedUpdate()
		{

		}

		public void FixedBehaviour()
		{
			transform.position = new Vector3(Target.position.x, zoom, Target.position.z);
		}
	}
}
