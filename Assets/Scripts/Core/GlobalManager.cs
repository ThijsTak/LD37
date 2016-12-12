using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Behaviours;
using Helpers;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
	public class GlobalManager : MonoBehaviour
	{
		public static GlobalManager Instance;
		public Base Home;
		public Player player;
		public MusicBuffer MusicBuffer;
		Queue<Collectable> muleOrderQueue = new Queue<Collectable>();
		List<Collectable> assignedItems = new List<Collectable>();
		public BasicSettings Settings = new BasicSettings();
		List<Mule> mules = new List<Mule>(10);
		List<Group> SpawnedGroups = new List<Group>(20);

		public bool PlayerPickedUp = false;
		public GameObject MulePrefab;

		public void Awake()
		{
			Instance = this;
		}

		void Update()
		{
			AssignOrders();
			if (player.GetEnergy() > 0) 
			{
				PlayerPickedUp = false;
			}

			CheckForVictory();
			CheckForDefeat();
		}

		public void RegisterMule(Mule mule)
		{
			mules.Add(mule);
		}

		public void AddToQueue(Collectable item)
		{
			if (muleOrderQueue.Contains(item) || assignedItems.Contains(item))
			{
				return;
			}

			muleOrderQueue.Enqueue(item);
		}

		public Collectable GetFromQueue()
		{
			if (muleOrderQueue.Count > 0)
			{
				// Not sure this is one will throw an exception.
				return muleOrderQueue.Dequeue();
			}

			return null;
		}

		public void FinishOrder(Collectable item)
		{
			if (assignedItems.Contains(item))
			{
				assignedItems.Remove(item);
			}
		}

		void AssignOrders()
		{
			while (muleOrderQueue.Count > 0 || player.Energy.Current == 0)
			{
				foreach (Mule mule in mules)
				{
					if (mule.Orders.Any())
					{
						continue;
					}

					if (player.Energy.Current == 0 && !PlayerPickedUp)
					{
						var pItem = player.gameObject.GetComponent<Collectable>();
						CollectObjectOrderHelper.CreateOrder(mule, pItem, Home);
						PlayerPickedUp = true;
						return;
					}

					var item = GetFromQueue();
					CollectObjectOrderHelper.CreateOrder(mule, item, Home);
					assignedItems.Add(item);
					return;
				}

				return;
			}
		}

		void CheckForVictory()
		{
			if (Home.GetShipComponentsState().Count(s => s.IsExisting) == 6)
			{
				// Ok, all components have been found.
				SceneManager.LoadScene(SceneHelper.Victory);
			}
		}

		void CheckForDefeat()
		{
			if (Home.Energy.Current == 0)
			{
				SceneManager.LoadScene(SceneHelper.GameOver);
			}
		}

		public void CreateMule()
		{
			GameObject.Instantiate(MulePrefab, Home.MuleSpawn.position, Quaternion.identity);
		}
	}
}

