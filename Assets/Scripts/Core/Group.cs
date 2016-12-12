using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using Behaviours;
using Behaviours.Enemies;
using Units;
using UnityEngine;

namespace Core
{
	[System.Serializable]
	public class Group
	{
		public List<Enemy> Enemies = new List<Enemy>(15);
		public EnemyStateInfo State = new EnemyStateInfo();
		public Spawners owner;

		public bool IsAlive
		{
			get { return 
					!Enemies.Any(e => e != null && e.gameObject.activeSelf) 
					&& owner != null; }
		}

		public void UpdateTask(Enemy enemy)
		{
			if (!IsAlive) return;

			switch (State.CurrentState)
			{
				case EnemyStateInfo.EnemyState.Idle:
					break;
				case EnemyStateInfo.EnemyState.Walking:
					break;
				case EnemyStateInfo.EnemyState.Roaming:
					CreateRoamingTask(enemy);
					break;
				case EnemyStateInfo.EnemyState.Hunting:
					break;
				case EnemyStateInfo.EnemyState.Attacking:
					break;
				case EnemyStateInfo.EnemyState.Running:
					break;
			}
		}

		void CreateRoamingTask(Enemy enemy)
		{
			// Ok, create a new movement target within the movement radius of the spawner.
			var target = (Random.insideUnitSphere * State.MaxRoamingRadius) + owner.transform.position;
			target = HeightHelper.HeightCorrect(target);
			enemy.behaviour.Enqueue(new MoveTo(target));
		}

		public void MassAssignOrders()
		{
			// if (!IsAlive) return;

			foreach (Enemy enemy in Enemies)
			{
				if (enemy == null || !enemy.gameObject.activeSelf) continue;
				UpdateTask(enemy);
			}
		}
	}
}
