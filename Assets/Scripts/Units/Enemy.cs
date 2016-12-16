using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Helpers;
using Behaviours.Enemies;
using Core;
using Helpers;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
	public class Enemy : BaseUnit
	{
		public Group Group;
		public float MovementSpeed = 5.0f;

		public Rigidbody body;

		public Queue<BaseBehaviour> behaviour = new Queue<BaseBehaviour>();
		public NavMeshAgent Agent;

		void Start()
		{
			//transform.position = new Vector3(
			//	transform.position.x,
			//	HeightHelper.GetHeightFromTerrain(transform.position),
			//	transform.position.z);

			body = GetComponent<Rigidbody>();

			Agent = GetComponent<NavMeshAgent>();
		}

		void Update()
		{
			//transform.position = new Vector3(
			//	transform.position.x,
			//	HeightHelper.GetHeightFromTerrain(transform.position + (Vector3.up * 5000)),
			//	transform.position.z);

			if (behaviour.Count == 0)
			{
				Group.UpdateTask(this);
				return;
			}

			if (behaviour.Peek().Update(this))
			{
				behaviour.Dequeue();
				Group.UpdateTask(this);
			}
		}

		public void MoveEnemy(Vector3 direction)
		{
			direction = direction.normalized;
			//var hits = Physics.RaycastAll(transform.position, direction, MovementSpeed * Time.deltaTime);
			//foreach (RaycastHit raycastHit in hits)
			//{
			//	if (raycastHit.collider.gameObject.layer == LayerHelper.StaticObject)
			//	{
			//		behaviour.Dequeue();
			//		return;
			//	}
			//}

			if (body.velocity.x != 0 && body.velocity.z != 0)
			{
				transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			}

			// transform.position = HeightHelper.HeightCorrect(transform.position + (direction * MovementSpeed * Time.deltaTime));
			body.velocity = direction * MovementSpeed;
		}
	}
}

