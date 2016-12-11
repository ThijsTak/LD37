using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Behaviours
{
	public class Collector : MonoBehaviour
	{
		public Base Home;

		public void OnTriggerStay(Collider collider)
		{
			Collectable drag = collider.gameObject.GetComponent<Collectable>();
			if (drag != null && drag.Transporter == null)
			{
				Home.AddEnergy(drag.EnergyValue);
				collider.gameObject.SendMessage("Death");
			}
		}
	}
}
