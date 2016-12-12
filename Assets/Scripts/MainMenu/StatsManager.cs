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
		TotalEnergyDrainedPlayer = 9
	}

	// Use this for initialization
	void Start ()
	{
		//GlobalManager manager = GlobalManager.Instance;
		//if (manager == null) return;
		StatCounter statsObject = StatCounter.Instance;
		switch (StatType)
		{
			case Stats.TotalTime:
				GetComponent<TextMesh>().text += statsObject.totalTime;
				break;
			case Stats.Pandicorns:
				GetComponent<TextMesh>().text += statsObject.PandiCorns;
				break;
			case Stats.Boosts:
				GetComponent<TextMesh>().text += statsObject.Boosts;
				break;
			case Stats.Tractors:
				GetComponent<TextMesh>().text += statsObject.Tractors;
				break;
			case Stats.EnergyUp:
				GetComponent<TextMesh>().text += statsObject.EnergyUp;
				break;
			case Stats.Mules:
				GetComponent<TextMesh>().text += statsObject.Mules;
				break;
			case Stats.TotalEnergyCollected:
				GetComponent<TextMesh>().text += statsObject.TotalEnergyCollected;
				break;
			case Stats.TotalEnergyDrainedBase:
				GetComponent<TextMesh>().text += statsObject.TotalEnergyDrainedBase;
				break;
			case Stats.TotalEnergyDrainedPlayer:
				GetComponent<TextMesh>().text += statsObject.TotalEnergyDrainedPlayer;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
