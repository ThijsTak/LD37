﻿using Behaviours;
using Helpers;
using Core;
using Helpers;
using UnityEngine;

namespace Units
{
	/// <summary>
	/// This class manages the player object and all of the unlocked abilities of the player.
	/// </summary>
	/// <seealso cref="BaseUnit" />
	[RequireComponent(typeof(Rigidbody))]
	public class Player : BaseUnit
	{
		[SerializeField]
		MinMaxValue Energy = new MinMaxValue();

		[SerializeField]
		TractorSystem TractorSystem = new TractorSystem();

		private float movementMultiplier = 2.0f;
		private float defaultDrag = 0;

		// Prefetched components.
		private Rigidbody body;
		private LineRenderer tractorLine;

		void Start()
		{
			body = GetComponent<Rigidbody>();
			if (body == null)
			{
				Debug.LogError("No body found for player object.");
			}

			tractorLine = GetComponent<LineRenderer>();
			if (body == null)
			{
				Debug.LogError("No body found for player object.");
			}

			if (defaultDrag == 0)
			{
				defaultDrag = body.drag;
			}
		}

		void FixedUpdate()
		{
			UpdateInput();
		}

		void UpdateInput()
		{
			Vector3 p = transform.position;

			// Update speed.
			Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			body.velocity += movement * movementMultiplier;

			// Get the mouse position relative to the camera.
			Vector3 hitPoint = MouseHelper.GetMousePosition() - new Vector3(p.x, 0, p.z);

			// Now rotate the onbject towards the point.
			gameObject.transform.rotation = Quaternion.LookRotation(hitPoint, Vector3.up);

			// Now check the actions.
			if (Input.GetButton("Stunner"))
			{
				ShootStunner();
			}

			if (Input.GetButton("Weapon"))
			{
				ShootWeapon();
			}

			if (Input.GetButtonDown("Grab"))
			{
				ActivateGrab();
			}

			if (Input.GetButtonDown("Order"))
			{
				ActivateOrder();
			}

			UpdateTractor();
		}

		/// <summary>
		/// Shoots the stunner.
		/// </summary>
		void ShootStunner()
		{

		}

		/// <summary>
		/// Shoots the weapon.
		/// </summary>
		void ShootWeapon()
		{

		}

		/// <summary>
		/// Activates the tractor beam.
		/// </summary>
		void ActivateGrab()
		{
			if (TractorSystem.Active)
			{
				TractorSystem.Target = null;
				TractorSystem.Active = false;
				return;
			}

			// Get all objects in range of the grabber.
			Collider[] objects = Physics.OverlapSphere(transform.position, TractorSystem.Radius);
			if (objects == null)
			{
				return;
			}


			Vector3 p = MouseHelper.GetMousePosition();
			foreach (Collider o in objects)
			{
				Draggable drag = o.GetComponent<Draggable>();
				if (drag == null)
				{
					continue;
				}

				TractorSystem.Active = true;
				TractorSystem.Target = drag.gameObject.transform;
				TractorSystem.TracRigidbody = drag.gameObject.GetComponent<Rigidbody>();
			}
		}

		/// <summary>
		/// Updates the tractor beam.
		/// </summary>
		void UpdateTractor()
		{
			tractorLine.enabled = TractorSystem.Active;
			tractorLine.SetPosition(0, transform.position);
			tractorLine.SetPosition(1, TractorSystem.Target != null ? TractorSystem.Target.position : transform.position);


			if (TractorSystem.Active)
			{
				float dist = Vector3.Distance(transform.position, TractorSystem.Target.position);

				if (dist > TractorSystem.Radius)
				{
					var direction = (transform.position - TractorSystem.Target.position).normalized;
					TractorSystem.TracRigidbody.velocity = direction * TractorSystem.Power;
					body.drag = defaultDrag + (transform.position - TractorSystem.Target.position).sqrMagnitude;
					return;
				}
			}
			body.drag = defaultDrag;
		}

		/// <summary>
		/// Activates the order.
		/// </summary>
		void ActivateOrder()
		{

		}
	}
}
