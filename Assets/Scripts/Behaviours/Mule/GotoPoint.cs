using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using UnityEngine;

namespace Behaviours.Mule
{
	public class GotoPoint : BehaviourState
	{
		public GotoPoint()
		{
			
		}

		public GotoPoint(Transform target)
		{
			Target = target;
		}

		public Transform Target;

		#region Overrides of BehaviourState

		public override bool Update(Units.Mule mule)
		{
			// Move towards the target.
			var dir = Vector3.Distance(Target.position, mule.transform.position);

			// Are we there yet?
			if (dir <= mule.Speed)
			{
				mule.Body.velocity = Vector3.zero;
				mule.transform.position = new Vector3(mule.transform.position.x,
					GlobalManager.Instance.Settings.MuleFlyHeight, mule.transform.position.z);

				return true;
			}
			else
			{
				mule.Body.velocity = new Vector3(0, GlobalManager.Instance.Settings.MuleAscentSpeed, 0);
				return false;
			}
		}

		public override void Abort()
		{
			
		}

		#endregion
	}
}
