using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using UnityEngine;

namespace Assets.Scripts.Core
{
	[Serializable]
	public class EnemyStateInfo
	{
		public enum EnemyState
		{
			/// <summary>
			/// Not doing anything special.
			/// </summary>
			Idle = 0,

			/// <summary>
			/// Walking towards a predefined spot.
			/// </summary>
			Walking = 1,

			/// <summary>
			/// Walking randomly.
			/// </summary>
			Roaming = 2,

			/// <summary>
			/// Hunting another group.
			/// </summary>
			Hunting = 3,

			/// <summary>
			/// Attacking the player.
			/// </summary>
			Attacking = 4,

			/// <summary>
			/// Running away from battle.
			/// </summary>
			Running = 5
		}

		/// <summary>
		/// The current state of the enemy.
		/// </summary>
		public EnemyState CurrentState = EnemyState.Roaming;

		public bool AwareOfPlayer = false;
		public Vector3 WalkTarget = Vector3.zero;
		public Group HuntGroup = null;
		public float MaxRoamingRadius = 50.0f;
	}
}
