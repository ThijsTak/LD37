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
			if (Body.velocity.x != 0 && Body.velocity.z != 0)
			{
				var target_rot = Quaternion.LookRotation (new Vector3 (Body.velocity.x, 0, Body.velocity.z));
				transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rot, Time.deltaTime * GlobalManager.Instance.Settings.MuleRotationSpeed);
			}

			if (Body.velocity.x != 0 && Body.velocity.z != 0)
			{
				transform.position = new Vector3(
					transform.position.x,
					HeightHelper.GetHeightFromTerrain(transform.position) + GlobalManager.Instance.Settings.MuleFlyHeight,
					transform.position.z);
			}
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
