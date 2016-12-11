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

		public enum CollectableType
		{
			Energy,
			Core,
			Engine1,
			Engine2,
			Hybernation,
			Navigation,
			Deflector
		}

		public void Start()
		{
			Body = GetComponent<Rigidbody>();
			Body.useGravity = Transporter == null;
			Body.drag = Transporter == null ? DefaultDrag : 0;
		}

		public void FixedUpdate()
		{
			
		}
	}
}
