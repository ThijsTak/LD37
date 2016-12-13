using Assets.Scripts.Behaviours;
using Core;
using UnityEngine;

namespace Units
{
	public abstract class BaseUnit : MonoBehaviour
	{
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public MinMaxValue Energy;

		public void DrainEnergy(float energy)
		{
			Energy.ChangeValue(-energy);
		}

		public void AddEnergy(float energy)
		{
			Energy.ChangeValue(energy);
		}

		public float GetEnergy()
		{
			return Energy.Current;
		}

		public float GetMaxEnegy()
		{
			return Energy.Max;
		}
	}
}
