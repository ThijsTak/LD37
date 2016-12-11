using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Units;
using UnityEngine;

namespace Behaviours
{
	public class Collector : MonoBehaviour
	{
		public void OnTriggerStay(Collider collider)
		{
			Collectable drag = collider.gameObject.GetComponent<Collectable>();
			if (drag != null && drag.Transporter == null)
			{
				switch (drag.Type)
				{
					case Collectable.CollectableType.Energy:
						GlobalManager.Instance.Home.AddEnergy(drag.EnergyValue);
						break;
					case Collectable.CollectableType.Core:
						GlobalManager.Instance.Home.EnergyCore = true;
						break;
					case Collectable.CollectableType.Engine1:
						GlobalManager.Instance.Home.Engine1 = true;
						break;
					case Collectable.CollectableType.Engine2:
						GlobalManager.Instance.Home.Engine2 = true;
						break;
					case Collectable.CollectableType.Hybernation:
						GlobalManager.Instance.Home.HybernationModule = true;
						break;
					case Collectable.CollectableType.Navigation:
						GlobalManager.Instance.Home.Navigation = true;
						break;
					case Collectable.CollectableType.Deflector:
						GlobalManager.Instance.Home.Deflector = true;
						break;
				}

				collider.gameObject.SendMessage("Death");
			}
		}

	}
}
