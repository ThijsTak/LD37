using UnityEngine;

namespace Behaviours
{
	public class SpawnOnDeath : MonoBehaviour
	{
		public GameObject Spawn = null;
		public int numberToSpawn = 1;

		void Death()
		{
			for (int i = 0; i < numberToSpawn; i++)
			{
				GameObject.Instantiate(Spawn);
			}
		}
	}
}