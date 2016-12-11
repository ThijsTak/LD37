using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class DestroyOnDeath : MonoBehaviour
	{
		public void Death()
		{
			GameObject.Destroy(gameObject);
		}
	}
}
