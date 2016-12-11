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

		public GameObject RoofShip;
		public GameObject RoofHybernation;
		public GameObject BottomHybernation;
		public GameObject Engine1Mesh;
		public GameObject Engine2Mesh;
		public GameObject NavigationMesh;
		public GameObject EnergyCodeMesh;
		public GameObject DeflectorMesh;

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
