using Core;
using UnityEngine;

namespace Behaviours
{
	public class Collectable : MonoBehaviour
	{
		public Rigidbody Body = null;
		public GameObject Transporter = null;
		public float DefaultDrag = 15.0f;

		public float EnergyValue = 10.0f;
		public CollectableType Type = CollectableType.Energy;
		public bool IsTagged = false;

		public bool AutoRegister = false;

		public enum CollectableType
		{
			Energy,
			Core,
			Engine1,
			Engine2,
			Hybernation,
			Navigation,
			Deflector,
			Player,
			FauxHybernation,
			Pandicorn,
			Boost,
			EnergyIncrease,
			Mules,
			Tractor
		}

		public void Start()
		{
			Body = GetComponent<Rigidbody>();

			if (Type == CollectableType.Player)
			{
				return;
			}

			Body.useGravity = Transporter == null;
			Body.drag = Transporter == null ? DefaultDrag : 0;

			if (AutoRegister)
			{
				GlobalManager.Instance.AddToQueue(this);
			}
		}

		public void FixedUpdate()
		{

		}
	}
}
