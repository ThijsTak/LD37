using Behaviours;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class SpawnEnergyOnDeath : MonoBehaviour
	{
		public GameObject Spawn = null;
		public int numberToSpawn = 3;
		public float energyPerSpawn = 10.0f;
		public float spawnMaxForce = 10.0f;

		void Death()
		{
			for (int i = 0; i < numberToSpawn; i++)
			{
				var blop = GameObject.Instantiate(Spawn, 
					gameObject.transform.position + (Vector3.up * 2), 
					gameObject.transform.rotation);

				var collect = blop.GetComponent<Collectable>();
				collect.EnergyValue = energyPerSpawn;

				var body = blop.GetComponent<Rigidbody>();
				body.AddForce(
					Random.Range(-spawnMaxForce, spawnMaxForce),
					Random.Range(0, spawnMaxForce),
					Random.Range(-spawnMaxForce, spawnMaxForce), ForceMode.Impulse);
			}
		}
	}
}
