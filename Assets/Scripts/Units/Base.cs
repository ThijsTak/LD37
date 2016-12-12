using Core;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
	public class Base : BaseUnit
	{
		public Transform DropPoint = null;

		// Main Components:
		public bool Engine1;
		public bool Engine2;
		public bool Navigation;
		public bool Deflector;
		public bool EnergyCore;
		public bool HybernationModule;

		public float RechanrgePlayerPerSecond = 25.0f;
		public float BaseEnegyDrainPerMinute = 100.0f;
		public float EngineAdditionalDrainPerMinute = 0.0f;
		public float NavigationAdditionalDrainPerMinute = 10.0f;
		public float DeflectorAdditionalDrainPerMinute = 15.0f;
		public float EnergyCodeAdditionalDrainPerMinute = -50.0f;
		public float HybernationAdditionalDrainPerMinute = 50.0f;
		public float EnergyPerMinutePerPandiCorn = 5.0f;

		public int TotalNumberOfPandiCorns = 0;

		public float totalEnergyDrainPerMinute;

		public GameObject RoofShip;
		public GameObject RoofHybernation;
		public GameObject BottomHybernation;
		public GameObject Engine1Mesh;
		public GameObject Engine2Mesh;
		public GameObject NavigationMesh;
		public GameObject EnergyCodeMesh;
		public GameObject DeflectorMesh;

		public Transform MuleSpawn;

		void Start()
		{
			// Register the base.
			GlobalManager.Instance.Home = this;
		}

		void FixedUpdate()
		{
			BottomHybernation.SetActive(HybernationModule);
			Engine1Mesh.SetActive(Engine1);
			Engine2Mesh.SetActive(Engine2);
			NavigationMesh.SetActive(Navigation);
			EnergyCodeMesh.SetActive(EnergyCore);
			DeflectorMesh.SetActive(Deflector);
			EnergyUpdate();
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

		public IEnumerable<BaseComponentState> GetShipComponentsState()
		{
			yield return new BaseComponentState("Engine 1", Engine1);
			yield return new BaseComponentState("Engine 2", Engine2);
			yield return new BaseComponentState("Navigation", Navigation);
			yield return new BaseComponentState("Deflector", Deflector);
			yield return new BaseComponentState("Energy Core", EnergyCore);
			yield return new BaseComponentState("Hybernation Module", HybernationModule);
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

		void EnergyUpdate()
		{
			totalEnergyDrainPerMinute =
				BaseEnegyDrainPerMinute
				+ (Engine1 ? EngineAdditionalDrainPerMinute : 0.0f)
				+ (Engine2 ? EngineAdditionalDrainPerMinute : 0.0f)
				+ (Navigation ? NavigationAdditionalDrainPerMinute : 0.0f)
				+ (Deflector ? DeflectorAdditionalDrainPerMinute : 0.0f)
				+ (EnergyCore ? EnergyCodeAdditionalDrainPerMinute : 0.0f)
				+ (HybernationModule ? HybernationAdditionalDrainPerMinute : 0.0f)
				- (TotalNumberOfPandiCorns * EnergyPerMinutePerPandiCorn);

			DrainEnergy((totalEnergyDrainPerMinute / 60) * Time.fixedDeltaTime);
		}
	}

	public class BaseComponentState
	{
		public string Name;
		public bool IsExisting;

		public BaseComponentState(string name, bool isExisting)
		{
			Name = name;
			IsExisting = isExisting;
		}
	}
}
