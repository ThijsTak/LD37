using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;

namespace Assets.Scripts.Behaviours.Mule
{
	public abstract class BehaviourState
	{
		public abstract bool Update(Units.Mule mule);
		public abstract void Abort();
	}
}
