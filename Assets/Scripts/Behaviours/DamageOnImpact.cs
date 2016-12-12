using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class DamageOnImpact : MonoBehaviour
	{
		public float Damage = 2.5f;
		public float Force = 5.0f;
		public float InvincibilityTime = 1000;
		private float hitTime = 0;

		void Update()
		{
			if (hitTime > 0)
			{
				hitTime -= Time.deltaTime*1000;
			}
		}

		void OnTriggerEnter(Collider collider)
		{
			if (collider.gameObject.tag == "Player" && hitTime <= 0)
			{
				Player player = collider.gameObject.GetComponent<Player>();
				player.DrainEnergy(Damage);
				player.body.AddForce(
					(player.transform.position - gameObject.transform.position).normalized * Force, 
					ForceMode.VelocityChange);
				hitTime = InvincibilityTime;
			}
		}
	}
}
