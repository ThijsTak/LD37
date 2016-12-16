using System.Security.Cryptography;
using Assets.Scripts.Helpers;
using Core;
using Units;
using UnityEngine;

namespace Behaviours.Enemies
{
	public class MoveTo : BaseBehaviour
	{
		public MoveTo(Vector3 target)
		{
			Target = target;
			time = GlobalManager.Instance.Settings.EnemyTimersBeforeNewOrderInSeconds;
		}

		public Vector3 Target;
		public float _dist;
		private bool MoveSet = false;
		private float time;

		#region Overrides of BaseBehaviour

		public override bool Update(Enemy enemy)
		{
			float dist = Vector2.Distance(QuickConvert(Target), QuickConvert(enemy.gameObject.transform.position));
			time -= Time.fixedDeltaTime;

			// Are we there yet?
			if (dist <= enemy.MovementSpeed) // * Time.fixedDeltaTime)
			{
				// enemy.transform.position = HeightHelper.HeightCorrect(Target);
				return true;
			}

			if (!MoveSet)
			{
				enemy.Agent.SetDestination(Target);
				MoveSet = true;
			}

			// enemy.Agent.set
			// enemy.Agent.acceleration = enemy.MovementSpeed;
			return false;
		}

		Vector2 QuickConvert(Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		#endregion
	}
}
