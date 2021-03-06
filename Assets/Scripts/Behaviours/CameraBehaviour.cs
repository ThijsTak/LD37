﻿using System;
using Core;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class CameraBehaviour : MonoBehaviour
	{
		public Transform Target;
		public BehaviourType Behaviour = BehaviourType.OverTheShoulder;

		public float TopDownZoom = 50;

		public float ShoulderHeight = 1.5f;
		public float ShoulderDistance = 2.5f;
		public float CameraMultiplier = 1.333f;
		public float TractorMultiplierDistance = 2.0f;
		public float TractorMulieplierHeight = 1.5f;

		public enum BehaviourType
		{
			TopDown = 0,
			OverTheShoulder = 1
		}

		public void Start()
		{
			TopDownZoom = transform.position.y;
		}

		public void LateUpdate()
		{
			switch (Behaviour)
			{
				case BehaviourType.TopDown:
					FixedBehaviour();
					break;
				case BehaviourType.OverTheShoulder:
					ShoulderBehaviour();
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
			transform.position = new Vector3(Target.position.x, TopDownZoom, Target.position.z);
			transform.rotation = Quaternion.Euler(90, 0, 0);
		}

		public void ShoulderBehaviour()
		{
			// This it the ideal camera position.
			bool isDragging = GlobalManager.Instance.player.TractorSystem.Active;
			Vector3 targetPosition = Target.transform.position + (Target.transform.rotation * (
				new Vector3(0, 
				ShoulderHeight * (isDragging ? TractorMulieplierHeight : 1), 
				-ShoulderDistance * (isDragging ? TractorMultiplierDistance : 1))));
			
			// Set the position.
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * CameraMultiplier);

			// Now set the lookat.
			transform.LookAt(Target, Vector3.up);
		}
	}
}
