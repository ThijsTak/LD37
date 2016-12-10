using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Behaviours.Mule
{
	public class DropCargo : BehaviourState
	{
		#region Overrides of BehaviourState

		public override bool Update(Units.Mule mule)
		{
			if (mule.Cargo == null)
			{
				return true;
			}

			mule.Cargo.body.useGravity = true;
			mule.Cargo = null;
			return true;
		}

		public override void Abort()
		{
			// Nothing to abort in a move order.
		}

		#endregion
	}
}
