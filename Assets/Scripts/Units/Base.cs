using Core;
using UnityEngine;

namespace Units
{
	public class Base : BaseUnit
	{
		public Transform DropPoint = null;
		public MinMaxValue Energy = new MinMaxValue();
		public float StoredMass;

		// Main Components:
		public bool Engine1;
		public bool Engine2;
		public bool Navigation;
		public bool Deflector;
		public bool EnergyCore;
		public bool HybernationModule;

		public void AddEnergy(float amount)
		{
			Energy.ChangeValue(amount);
		}

		public void RemoveEnergy(float amount)
		{
			Energy.ChangeValue(-amount);
		}
	}
}
