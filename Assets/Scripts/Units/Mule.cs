using System.Collections.Generic;
using Assets.Scripts.Behaviours.Mule;
using Core;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Units
{
	public class Mule : BaseUnit
	{
		[SerializeField]
		MinMaxValue Health;
		[SerializeField]
		Base Home;
		[SerializeField]
		Queue<BehaviourState> Orders;

		public void Start()
		{

		}

		public void Update()
		{

		}

		public void FixedUpdate()
		{

		}
	}
}
