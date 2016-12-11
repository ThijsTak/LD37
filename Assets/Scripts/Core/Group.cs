using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Units;

namespace Core
{
	[Serializable]
	public class Group
	{
		public List<Enemy> Enemies = new List<Enemy>(15);
		public EnemyStateInfo State;

		public bool IsAlive { get { return true; } }
	}
}
