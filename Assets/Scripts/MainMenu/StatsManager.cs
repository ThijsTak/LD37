using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class StatsManager : MonoBehaviour
{

	public Stats StatType;

	public enum Stats
	{
		TotalTime = 1,
		Pandicorns = 2,
		Boosts = 3,
		Tractors = 4,
		EnergyUp = 5,
		Mules = 6,
		TotalEnergyCollected = 7,
		TotalEnergyDrainedBase = 8,
		TotalEnergyDrainedPlayer = 9,
		TotalEnergyGlobes = 10
	}

	// Use this for initialization
	void Start()
	{
		//GlobalManager manager = GlobalManager.Instance;
		//if (manager == null) return;
		StatCounter statsObject = StatCounter.Instance;
		switch (StatType)
		{
			case Stats.TotalTime:
				var span = TimeSpan.FromSeconds(statsObject.totalTime);
				GetComponent<TextMesh>().text += string.Format("{0} : {1} : {2}", span.Hours, span.Minutes, span.Seconds);
				break;
			case Stats.Pandicorns:
				GetComponent<TextMesh>().text += statsObject.PandiCorns + " of " + statsObject.TotalPandas;
				break;
			case Stats.Boosts:
				GetComponent<TextMesh>().text += statsObject.Boosts + " of " + statsObject.TotalBoosts;
				break;
			case Stats.Tractors:
				GetComponent<TextMesh>().text += statsObject.Tractors + " of "+ statsObject.TotalTractors;
				break;
			case Stats.EnergyUp:
				GetComponent<TextMesh>().text += statsObject.EnergyUp + " of " + statsObject.TotalEnergyUp;
				break;
			case Stats.Mules:
				GetComponent<TextMesh>().text += statsObject.Mules + " of " + statsObject.TotalMules;
				break;
			case Stats.TotalEnergyCollected:
				GetComponent<TextMesh>().text += string.Format("{0:n0}", statsObject.TotalEnergyCollected);
				break;
			case Stats.TotalEnergyDrainedBase:
				GetComponent<TextMesh>().text += string.Format("{0:n0}", statsObject.TotalEnergyDrainedBase);
				break;
			case Stats.TotalEnergyDrainedPlayer:
				GetComponent<TextMesh>().text += string.Format("{0:n0}", statsObject.TotalEnergyDrainedPlayer);
				break;
			case Stats.TotalEnergyGlobes:
				GetComponent<TextMesh>().text += statsObject.TotalEnergyGlobesCollected;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
