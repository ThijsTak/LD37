using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using UnityEngine;

namespace Behaviours
{
	class StatRegister : MonoBehaviour
	{
		public enum RegisterType
		{
			Boost,
			Energy,
			Tractor,
			Panda,
			Mule
		}

		public RegisterType type;

		public void Start()
		{
			switch (type)
			{
				case RegisterType.Boost:
					StatCounter.Instance.TotalBoosts++;
					break;
				case RegisterType.Energy:
					StatCounter.Instance.TotalEnergyUp++;
					break;
				case RegisterType.Tractor:
					StatCounter.Instance.TotalTractors++;
					break;
				case RegisterType.Panda:
					StatCounter.Instance.TotalPandas++;
					break;
				case RegisterType.Mule:
					StatCounter.Instance.TotalMules++;
					break;
			}
		}
	}
}
