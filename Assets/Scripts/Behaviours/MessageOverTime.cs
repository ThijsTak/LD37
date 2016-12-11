using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class MessageOverTime : MonoBehaviour
	{
		public float TimeInSeconds = 1.0f;
		public string Message = "Death";
		public bool Repeat = false;
		public bool Enabled = true;

		float deltaTime = 0.0f;

		void FixedUpdate()
		{
			if (!enabled) return;

			deltaTime += Time.fixedDeltaTime;
			if (TimeInSeconds < deltaTime)
			{
				SendMessage(Message);
				if (Repeat)
				{
					deltaTime = 0.0f;
				}
				else
				{
					enabled = false;
				}
			}
		}
	}
}
