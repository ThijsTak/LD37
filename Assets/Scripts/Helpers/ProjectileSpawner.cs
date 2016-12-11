using Core;
using UnityEngine;

namespace Helpers
{
	public static class ProjectileSpawner
	{
		public static GameObject SpawnProjectile(GameObject prefab, Vector3 position, Vector3 direction, float speed, float power, Damage.PowerType type)
		{
			var newObject = GameObject.Instantiate(prefab, position, Quaternion.identity) as GameObject;

			var body = newObject.GetComponent<Rigidbody>();
			if (body != null)
			{
				body.velocity = direction * speed;
			}

			var damage = newObject.GetComponent<Damage>();
			if (damage != null)
			{
				damage.Power = power;
				damage.Type = type;
			}

			return newObject; 
		}
	}
}
