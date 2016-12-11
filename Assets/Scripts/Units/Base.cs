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

		public GameObject RoofShip;
		public GameObject RoofHybernation;
		public GameObject BottomHybernation;
		public GameObject Engine1Mesh;
		public GameObject Engine2Mesh;
		public GameObject NavigationMesh;
		public GameObject EnergyCodeMesh;

		void Start()
		{
			// Register the base.
			GlobalManager.Instance.Home = this;
		}


		public void AddEnergy(float amount)
		{
			Energy.ChangeValue(amount);
		}

		public void RemoveEnergy(float amount)
		{
			Energy.ChangeValue(-amount);
		}

		void OnTriggerEnter(Collider collider)
		{
			if (collider.gameObject.tag == "Player")
			{
				RoofShip.SetActive(false);
				if (HybernationModule)
				{
					RoofHybernation.SetActive(false);
				}
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (collider.gameObject.tag == "Player")
			{
				RoofShip.SetActive(true);
				if (HybernationModule)
				{
					RoofHybernation.SetActive(true);
				}
			}
		}
	}
}
