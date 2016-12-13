using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{

	[Serializable]
	public class StatCounter : MonoBehaviour
	{
		//public StatCounter()
		//{
		//	Instance = this;
		//}

		public enum Difficulty
		{
			Easy,
			Medium,
			Hard
		}

		public static StatCounter Instance;

		public Difficulty SelecteDifficulty;

		public float totalTime = 0.0f;
		public int PandiCorns = 0;
		public int Boosts = 0;
		public int Tractors = 0;
		public int EnergyUp = 0;
		public int Mules = 2;
		public float TotalEnergyCollected = 0;
		public float TotalEnergyDrainedBase = 0.0f;
		public float TotalEnergyDrainedPlayer = 0.0f;
		public int TotalEnergyGlobesCollected = 0;

		public int TotalBoosts = 0;
		public int TotalTractors = 0;
		public int TotalEnergyUp = 0;
		public int TotalMules = 0;
		public int TotalPandas = 0;

		public void Awake()
		{
			if (Instance == null)
			{
				DontDestroyOnLoad(gameObject);
				Instance = this;
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}
		}
	}
}
