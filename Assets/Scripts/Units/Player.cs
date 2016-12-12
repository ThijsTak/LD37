using Assets.Scripts.Helpers;
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
		[SerializeField]
		public TractorSystem TractorSystem = new TractorSystem();
		[SerializeField]
		private Weapon Stunner;
		[SerializeField]
		private Weapon Blaster;
		[SerializeField]
		private float movementMultiplier = 2.0f;
		[SerializeField]
		private float turnMultiplier = 0.5f;
		[SerializeField]
		private float defaultDrag = 0;

		// Prefetched components.
		public Rigidbody body;
		private LineRenderer tractorLine;
		public bool CanBoost = true;
		public float BoostMulieplier = 2.0f;

		Vector2 tractOffset = Vector2.zero;

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		void Start()
		{
			GlobalManager.Instance.player = this;

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
			if (Energy.Current == 0)
			{
				if (!GlobalManager.Instance.PlayerPickedUp)
				{
					transform.position = new Vector3(
						transform.position.x,
						HeightHelper.GetHeightFromTerrain(transform.position) - 1.5f,
						transform.position.z);
				}
				return;
			}

			// Update the weapons.
			if (Blaster != null)
			{
				Blaster.Update(Time.deltaTime);
			}

			//if (Stunner != null)
			//{
			//	Stunner.Update(Time.deltaTime);
			//}

			// Set height.
			transform.position = new Vector3(
				transform.position.x,
				HeightHelper.GetHeightFromTerrain(transform.position) + GlobalManager.Instance.Settings.PlayerHoverHeight,
				transform.position.z);
		}

		/// <summary>
		/// Fixed update step (physics update)
		/// </summary>
		void FixedUpdate()
		{
			if (Energy.Current == 0)
			{
				return;
			}

			UpdateInput();
		}

		/// <summary>
		/// Handle the user input.
		/// </summary>
		void UpdateInput()
		{
			Vector3 p = transform.position;

			// Update speed.
			Vector3 movement = transform.rotation * (Vector3.forward * Input.GetAxis("Vertical"));
			body.velocity += movement * movementMultiplier *
				(CanBoost && Input.GetButton("Boost") ? BoostMulieplier : 1);
			transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * turnMultiplier);

			// Now check the actions.
			//if (Input.GetButton("Stunner"))
			//{
			//	ShootStunner();
			//}

			if (Input.GetButton("Weapon"))
			{
				ShootWeapon();
			}

			if (Input.GetButtonDown("Grab"))
			{
				ActivateGrab();
			}

			//if (Input.GetButtonDown("Order"))
			//{
			//	ActivateOrder();
			//}

			UpdateTractor();
		}

		///// <summary>
		///// Shoots the stunner.
		///// </summary>
		//void ShootStunner()
		//{
		//	if (Stunner != null)
		//	{
		//		// Get the mouse position relative to the camera.
		//		// Vector3 hitPoint = MouseHelper.GetMousePosition() - new Vector3(transform.position.x, 0, transform.position.z);

		//		Vector3 hitPoint;

		//		RaycastHit hit;
		//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//		if (Physics.Raycast(ray, out hit))
		//		{
		//			Transform objectHit = hit.transform;
		//			hitPoint = hit.point;
		//		}
		//		else
		//		{
		//			hitPoint = transform.rotation * Vector3.forward;
		//		}

		//		Stunner.Shoot(hitPoint);
		//	}
		//}

		/// <summary>
		/// Shoots the weapon.
		/// </summary>
		void ShootWeapon()
		{
			if (Blaster != null)
			{

				Vector3 hitPoint;

				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit))
				{
					Transform objectHit = hit.transform;
					hitPoint = hit.point;
				}
				else
				{
					hitPoint = transform.rotation * Vector3.forward;
				}

				Blaster.Shoot(hitPoint);
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
				if (drag == null || drag.Type == Collectable.CollectableType.Player)
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
			tractorLine.SetPosition(0, transform.position + Vector3.up);
			tractorLine.SetPosition(1, TractorSystem.Target != null ? TractorSystem.Target.position : transform.position);

			tractOffset = tractOffset + (Vector2.right * Time.deltaTime);
			tractorLine.material.SetTextureOffset("_MainTex", tractOffset);

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
