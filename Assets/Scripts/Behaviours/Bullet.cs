using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Units;
using UnityEngine;

namespace Behaviours
{
	public class Bullet : MonoBehaviour
	{
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
