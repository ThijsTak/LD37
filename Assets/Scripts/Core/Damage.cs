using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	public class Damage
	{
		public float Power;
		public PowerType Type;

		public enum PowerType
		{
			Normal = 0,
			Stun = 1
		}
	}
}
