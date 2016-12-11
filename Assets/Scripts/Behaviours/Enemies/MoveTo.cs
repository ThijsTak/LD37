using System.Security.Cryptography;
using Assets.Scripts.Helpers;
using Units;
using UnityEngine;

namespace Behaviours.Enemies
{
	public class MoveTo : BaseBehaviour
	{
		public MoveTo(Vector3 target)
		{
			Target = target;
		}

		public Vector3 Target;
		public float _dist;

		#region Overrides of BaseBehaviour

		public override bool Update(Enemy enemy)
		{
			float dist = Vector2.Distance(QuickConvert(Target), QuickConvert(enemy.gameObject.transform.position));

			// Are we there yet?
			if (dist <= enemy.MovementSpeed) // * Time.fixedDeltaTime)
			{
				enemy.transform.position = HeightHelper.HeightCorrect(Target);
				return true;
			}

			// Move forward!!!! Dum. Dum. Dum. Dum, dumdum. Dum, dumdum.
			// enemy.body.velocity = (Target - enemy.transform.position).normalized * enemy.MovementSpeed;
			var x = (Target - enemy.transform.position);
			enemy.MoveEnemy(new Vector3(x.x, 0, x.z).normalized);
			return false;
		}

		Vector2 QuickConvert(Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		#endregion
	}
}
