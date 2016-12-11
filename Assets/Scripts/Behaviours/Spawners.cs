using System.Collections.Generic;
using Core;
using Units;
using UnityEngine;

namespace Behaviours
{
	public class Spawners : MonoBehaviour
	{
		public Enemy BaseMonster = null;
		public Vector2 NumberPerGroup;
		public float radius = 7.5f;
		public int maxNumberOfActiveGroups = 3;
		public float secondsBetweenSpawns = 60.0f;
		public float maxDistanceOfPlayer = 0f;

		private float deltaTime = 0f;
		List<Group> linkedGroups = new List<Group>();

		void FixedUpdate()
		{
			var player = GlobalManager.Instance.player;

			// Check if the player is in range.
			if (maxDistanceOfPlayer != 0 &&
				Vector3.Distance(player.gameObject.transform.position, transform.position) > maxDistanceOfPlayer)
			{
				return;
			}

			// Ok, add the delta time.
			deltaTime += Time.fixedDeltaTime;

			if (deltaTime < secondsBetweenSpawns)
			{
				return;
			}

			if (linkedGroups.Count >= maxNumberOfActiveGroups)
			{
				for (int i = linkedGroups.Count - 1; i >= 0; i--)
				{
					if (!linkedGroups[i].IsAlive)
					{
						linkedGroups.RemoveAt(i);
					}
				}
			}

			if (linkedGroups.Count >= maxNumberOfActiveGroups)
			{
				return;
			}

			// Ok, spawn new monsters.
			int number = (int)Random.Range(NumberPerGroup.x, NumberPerGroup.y);
			Group group = new Group();
			linkedGroups.Add(group);

			for (int i = 0; i < number; i++)
			{
				Vector3 offset = Random.insideUnitSphere * radius;
				Enemy enemy = GameObject.Instantiate(BaseMonster, transform.position + offset, Quaternion.identity);
				group.Enemies.Add(enemy);
				enemy.Group = group;
			}

			deltaTime = 0.0f;
		}
	}
}
