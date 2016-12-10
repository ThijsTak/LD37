﻿using System.Collections.Generic;
using Behaviours;
using Behaviours.Mule;
using Core;
using UnityEngine;

namespace Units
{
	public class Mule : BaseUnit
	{
		public MinMaxValue Health = new MinMaxValue();
		public Queue<BehaviourState> Orders = new Queue<BehaviourState>();
		public float Speed = 0.0f;
		public Draggable Cargo = null;
		public GameObject Dock = null;

		public Rigidbody Body = null;

		public void Start()
		{
			Body = GetComponent<Rigidbody>();
			GlobalManager.Instance.RegisterMule(this);
		}

		public void Update()
		{

		}

		public void FixedUpdate()
		{
			if (Orders.Count > 0)
			{
				if (Orders.Peek().Update(this))
				{
					Orders.Dequeue();
				}
			}

			if (Cargo != null)
			{
				Cargo.transform.position = Dock.transform.position;
				Cargo.transform.rotation = Dock.transform.rotation;
			}
		}
	}
}
