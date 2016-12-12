using System.Collections.Generic;
using Assets.Scripts.Helpers;
using Behaviours;
using Behaviours.Mule;
using Core;
using UnityEngine;

namespace Units
{
	public class Mule : BaseUnit
	{
		public MinMaxValue Health = new MinMaxValue();
		public Queue<BehaviourState> Orders = new Queue<BehaviourState>();
		public Collectable Cargo = null;
		public GameObject Dock = null;

		public Rigidbody Body = null;

		public void Start()
		{
			Body = GetComponent<Rigidbody>();
			GlobalManager.Instance.RegisterMule(this);
		}

		public void Update()
		{
			// We are moving, make the mule face the right direction.
			if (Body.velocity.x != 0 || Body.velocity.z != 0)
			{
				// Because we are moving we need to let mule face the direction of the velocity.
				var target_rot = Quaternion.LookRotation (new Vector3 (Body.velocity.x, 0, Body.velocity.z));
				transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rot, Time.deltaTime * GlobalManager.Instance.Settings.MuleRotationSpeed);
			
				var centerOfFlightCorridor = HeightHelper.GetHeightFromTerrain(transform.position) + GlobalManager.Instance.Settings.MuleFlyHeight;
				var currentHeight = transform.position.y; 


				if (IsBelowFlightCorridor(centerOfFlightCorridor, currentHeight)) {
					// Increase Y position.
					var target_pos = new Vector3(
						transform.position.x,
						centerOfFlightCorridor,
						transform.position.z);
					transform.position = Vector3.Lerp (transform.position, target_pos, Time.deltaTime);
				} else if (IsAboveFlightCorridor(centerOfFlightCorridor, currentHeight)) {
					// Decrease Y position.
					var target_pos = new Vector3(
						transform.position.x,
						centerOfFlightCorridor,
						transform.position.z);
					transform.position = Vector3.Lerp (transform.position, target_pos, Time.deltaTime);
				}else {
					// We are inside the flight corridor.
					return;
				}
			}
		}

		private bool IsBelowFlightCorridor(float centerOfCorridor, float currentHeight) {
			return (centerOfCorridor - GlobalManager.Instance.Settings.MuleFlightCorridorHeight / 2) > currentHeight;
		}

		private bool IsAboveFlightCorridor(float centerOfCorridor, float currentHeight) {
			return (centerOfCorridor + GlobalManager.Instance.Settings.MuleFlightCorridorHeight / 2) < currentHeight;
		}

		public void FixedUpdate()
		{
			if (Orders.Count > 0)
			{
				if (Orders.Peek().Update(this))
				{
					Orders.Dequeue();
				}
			}

			if (Cargo != null)
			{
				Cargo.transform.position = Dock.transform.position;
				Cargo.transform.rotation = Dock.transform.rotation;
			}
		}
	}
}
