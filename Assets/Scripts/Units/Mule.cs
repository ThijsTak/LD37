using System.Collections.Generic;
using Behaviours;
using Behaviours.Mule;
using Core;
using UnityEngine;

namespace Units
{
	public class Mule : BaseUnit
	{
		public MinMaxValue Health = new MinMaxValue();
		public Base Home = null;
		public Queue<BehaviourState> Orders = new Queue<BehaviourState>();
		public float Speed = 0.0f;
		public float FloatHeight = 0;
		public Draggable Cargo = null;

		public Rigidbody Body = null;

		public void Start()
		{
			Body = GetComponent<Rigidbody>();
		}

		public void Update()
		{

		}

		public void FixedUpdate()
		{

		}
	}
}
