using System.Collections.Generic;
using System.Linq;
using Behaviours;
using Helpers;
using Units;
using UnityEngine;

namespace Core
{
	public class GlobalManager : MonoBehaviour
	{
		public static GlobalManager Instance;
		public Base Home;
		Queue<Draggable> muleOrderQueue = new Queue<Draggable>();
		List<Draggable> assignedItems = new List<Draggable>();
		public BasicSettings Settings = new BasicSettings();
		List<Mule> mules = new List<Mule>(10);


		public void Awake()
		{
			Instance = this;
		}

		void Update()
		{
			AssignOrders();
		}

		public void RegisterMule(Mule mule)
		{
			mules.Add(mule);
		}

		public void AddToQueue(Draggable item)
		{
			if (muleOrderQueue.Contains(item) || assignedItems.Contains(item))
			{
				return;
			}

			muleOrderQueue.Enqueue(item);
		}

		public Draggable GetFromQueue()
		{
			if (muleOrderQueue.Count > 0)
			{
				// Not sure this is one will throw an exception.
				return muleOrderQueue.Dequeue();
			}

			return null;
		}

		public void FinishOrder(Draggable item)
		{
			if (assignedItems.Contains(item))
			{
				assignedItems.Remove(item);
			}
		}

		void AssignOrders()
		{
			while (muleOrderQueue.Count > 0)
			{
				foreach (Mule mule in mules)
				{
					if (mule.Orders.Any())
					{
						continue;
					}

					var item = GetFromQueue();
					CollectObjectOrderHelper.CreateOrder(mule, item, Home);
					assignedItems.Add(item);
					return;
				}

				return;
			}
		}


	}
}
