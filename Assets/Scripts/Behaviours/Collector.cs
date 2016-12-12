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
			var player = collider.GetComponent<Player>();
			if (player != null && player.Energy.Current < player.GetMaxEnegy())
			{
				var drain = GlobalManager.Instance.Home.RechanrgePlayerPerSecond * Time.deltaTime;
				GlobalManager.Instance.Home.DrainEnergy(drain);
				player.AddEnergy(drain);
			}

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
					case Collectable.CollectableType.Player:
						return;
					case Collectable.CollectableType.FauxHybernation:
						return;
					case Collectable.CollectableType.Pandicorn:
						GlobalManager.Instance.Home.TotalNumberOfPandiCorns++;
						break;
					case Collectable.CollectableType.Mules:

						break;
					case Collectable.CollectableType.EnergyIncrease:
						GlobalManager.Instance.player.Energy.Max +=
							GlobalManager.Instance.Settings.EnergyGainPerUpgrade;
						break;
					case Collectable.CollectableType.Boost:
						GlobalManager.Instance.player.BoostMulieplier +=
							GlobalManager.Instance.Settings.BoostSpeedPerUpgrade;
						break;
				}

				collider.gameObject.SendMessage("Death");
			}
		}

	}
}
