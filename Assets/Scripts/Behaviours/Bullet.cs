
using Core;
using Units;
using UnityEngine;

namespace Behaviours
{
	public class Bullet : MonoBehaviour
	{
		void Start()
		{
			var source = GetComponent<AudioSource>();
			source.pitch = Random.Range(0.75f, 1.25f);
		}

		void OnTriggerEnter(Collider collider)
		{
			var unit = collider.gameObject.GetComponent<BaseUnit>();
			if (unit == null) return;

			var damage = GetComponent<Damage>();
			unit.DrainEnergy(damage.Power);

			SendMessage("Death");
		}
	}
}
