using Behaviours;
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
		[SerializeField] MinMaxValue Energy = new MinMaxValue();
		[SerializeField] TractorSystem TractorSystem = new TractorSystem();
		[SerializeField] private Weapon Stunner;
		[SerializeField] private Weapon Blaster;
		[SerializeField] private float movementMultiplier = 2.0f;
		[SerializeField] private float defaultDrag = 0;

		// Prefetched components.
		private Rigidbody body;
		private LineRenderer tractorLine;

		/// <summary>
		/// Initialize this instance.
		/// </summary>
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

			Stunner.Init(this);
			Blaster.Init(this);
		}

		/// <summary>
		/// Updates this pixels.
		/// </summary>
		void Update()
		{
			// Update the weapons.
			if (Blaster != null)
			{
				Blaster.Update(Time.deltaTime);
			}

			if (Stunner != null)
			{
				Stunner.Update(Time.deltaTime);
			}
		}

		/// <summary>
		/// Fixed update step (physics update)
		/// </summary>
		void FixedUpdate()
		{
			UpdateInput();
		}

		/// <summary>
		/// Handle the user input.
		/// </summary>
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
			if (Stunner != null)
			{
				Stunner.Shoot();
			}
		}

		/// <summary>
		/// Shoots the weapon.
		/// </summary>
		void ShootWeapon()
		{
			if (Blaster != null)
			{
				Blaster.Shoot();
			}
		}

		/// <summary>
		/// Activates the tractor beam.
		/// </summary>
		void ActivateGrab()
		{
			if (TractorSystem.Active)
			{
				Collectable coll = TractorSystem.Target.gameObject.GetComponent<Collectable>();
				if (coll != null)
				{
					coll.Transporter = null;
				}

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
				Collectable drag = o.GetComponent<Collectable>();
				if (drag == null)
				{
					continue;
				}

				TractorSystem.Active = true;
				TractorSystem.Target = drag.gameObject.transform;
				TractorSystem.TracRigidbody = drag.gameObject.GetComponent<Rigidbody>();
				drag.Transporter = gameObject;
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

				var col = TractorSystem.Target.gameObject.GetComponent<Collectable>();
				if (col != null && col.Transporter != gameObject)
				{
					ActivateGrab();
					return;
				}

				if (dist > TractorSystem.Radius)
				{
					Vector3 direction = (transform.position - TractorSystem.Target.position).normalized;
					TractorSystem.TracRigidbody.velocity = direction * TractorSystem.Power;
					body.drag = defaultDrag + ((transform.position - TractorSystem.Target.position).sqrMagnitude / TractorSystem.Power);
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
			// Replace this with another radius.
			float radius = 10.0f;
			Vector3 p = MouseHelper.GetMousePosition();

			// Get all objects in range of the grabber.
			Collider[] objects = Physics.OverlapSphere(p, radius);
			if (objects == null)
			{
				return;
			}

			foreach (Collider o in objects)
			{
				Collectable drag = o.GetComponent<Collectable>();
				if (drag == null)
				{
					continue;
				}

				GlobalManager.Instance.AddToQueue(drag);
			}
		}
	}
}
