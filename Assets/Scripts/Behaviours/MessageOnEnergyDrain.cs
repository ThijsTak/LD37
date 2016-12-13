using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class MessageOnEnergyDrain : MonoBehaviour
	{
		public string Message = "Death";
		public BaseUnit unit = null;

		void Start()
		{
			if (unit == null)
			{
				unit = GetComponent<BaseUnit>();
			}
		}

		void FixedUpdate()
		{
			if (unit.GetEnergy() == 0)
			{
				SendMessage(Message); 
			}
		}
	}
}
