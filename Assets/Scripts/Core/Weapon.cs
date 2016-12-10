using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;
using Units;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class Weapon
	{
		public float Power = 25;
		public float EnergyPerShot = 0.25f;
		public float ProjectileSpeed = 15.0f;
		public Transform ShotPoint = null;
		public GameObject ProjectilePrefab = null;
		public Damage.PowerType Type = Damage.PowerType.Normal;

		public float ShotsPerMinute = 0;
		public float Timer = 0;

		float rof;
		private BaseUnit owner;

		public void Init(BaseUnit unit)
		{
			rof = 60 / ShotsPerMinute;
			owner = unit;
		}

		public void Update(float deltaTime)
		{
			Timer += deltaTime;
		}

		public void Shoot()
		{
			if (!(Timer >= rof))
			{
				return;
			}

			Timer = 0;

			ProjectileSpawner.SpawnProjectile(
				ProjectilePrefab,
				ShotPoint.transform.position,
				(owner.gameObject.transform.rotation * Vector3.forward),
				ProjectileSpeed,
				Power,
				Type);

			owner.DrainEnergy(EnergyPerShot);
		}
	}
}
