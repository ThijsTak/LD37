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

			player = GlobalManager.Instance.player;
			Collectable drag = collider.gameObject.GetComponent<Collectable>();
			if (drag != null && (drag.Transporter == null || drag.Transporter == player.gameObject))
			{
				if (drag.Transporter == player.gameObject)
				{
					player.TractorSystem.Target = null;
					player.TractorSystem.Active = false;
				}

				switch (drag.Type)
				{
					case Collectable.CollectableType.Energy:
						GlobalManager.Instance.Home.AddEnergy(drag.EnergyValue);
						StatCounter.Instance.TotalEnergyGlobesCollected++;
						StatCounter.Instance.TotalEnergyCollected += drag.EnergyValue;
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
						StatCounter.Instance.PandiCorns++;
						break;
					case Collectable.CollectableType.Mules:
						GlobalManager.Instance.CreateMule();
						StatCounter.Instance.Mules++;
						break;
					case Collectable.CollectableType.EnergyIncrease:
						GlobalManager.Instance.player.Energy.Max +=
							GlobalManager.Instance.Settings.EnergyGainPerUpgrade;
						StatCounter.Instance.EnergyUp++;
						break;
					case Collectable.CollectableType.Boost:
						if (!GlobalManager.Instance.player.CanBoost)
						{
							GlobalManager.Instance.player.CanBoost = true;
						}
						else
						{
							GlobalManager.Instance.player.BoostMulieplier +=
								GlobalManager.Instance.Settings.BoostSpeedPerUpgrade;
						}
						StatCounter.Instance.Boosts++;
						break;
					case Collectable.CollectableType.Tractor:
						GlobalManager.Instance.player.TractorSystem.Power +=
							GlobalManager.Instance.Settings.TractorIncrease;
						StatCounter.Instance.Tractors++;
						break;
				}

				collider.gameObject.SendMessage("Death");
			}
		}

	}
}
