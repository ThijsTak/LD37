using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Behaviours;
using Helpers;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR.WSA.WebCam;

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
		// public StatCounter statCounter = StatCounter.Instance;

		public bool PlayerPickedUp = false;
		public GameObject MulePrefab;

		public RadarView Radar;

		private bool isGameRunning = true;

		public Text PickupText;

		public void Awake()
		{
			// Object.DontDestroyOnLoad(gameObject);

			// if (Instance != null)
			// {
			// 	GameObject.Destroy(Instance);
			// }

			Instance = this;
		}

		void OnStart()
		{
			switch (StatCounter.Instance.SelecteDifficulty)
			{
				case StatCounter.Difficulty.Easy:
					Home.Energy.Current = 7500;

					CreateMule();
					player.CanBoost = true;
					player.TractorSystem.Power += Settings.TractorIncrease;
					Radar.Categories.Single(r => r.Name == "Collectable").MaxDistance += 250;
					break;
				case StatCounter.Difficulty.Medium:
					Home.Energy.Current = 5000;
					break;
				case StatCounter.Difficulty.Hard:
					Home.Energy.Current = 2500;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		void Update()
		{
			if (!isGameRunning)
			{
				return;
			}

			AssignOrders();
			if (player.GetEnergy() > 0)
			{
				PlayerPickedUp = false;
			}

			StatCounter.Instance.totalTime += Time.deltaTime;

			if (player.Energy.Current == 0)
			{
				if (PlayerPickedUp)
				{
					PickupText.text = "Mule underway";
				}
				else
				{
					PickupText.text = "pickup pending";
				}
			}
			else
			{
				PickupText.text = "";
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
				isGameRunning = false;
			}
		}

		void CheckForDefeat()
		{
			if (Home.Energy.Current == 0)
			{
				SceneManager.LoadScene(SceneHelper.GameOver);
				isGameRunning = false;
			}
		}

		public void CreateMule()
		{
			GameObject.Instantiate(MulePrefab, Home.MuleSpawn.position, Quaternion.identity);
		}
	}
}

