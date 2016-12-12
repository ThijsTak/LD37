using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	[Serializable]
	public class StatCounter
	{
		public float totalTime = 0.0f;
		public int PandiCorns = 0;
		public int Boosts = 0;
		public int Tractors = 0;
		public int EnergyUp = 0;
		public int Mules = 2;
		public float TotalEnergyCollected = 0;
		public float TotalEnergyDrainedBase = 0.0f;
		public float TotalEnergyDrainedPlayer = 0.0f;
	}
}
