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
			Vector3 v = new Vector3(Target.position.x,
						GlobalManager.Instance.Settings.MuleFlyHeight,
						Target.position.z);

			float dir = Vector3.Distance(v, mule.transform.position);

			// Are we there yet?
			if (dir <= mule.Speed * Time.fixedDeltaTime)
			{
				mule.Body.velocity = Vector3.zero;
				mule.transform.position = new Vector3(Target.position.x, mule.transform.position.y, Target.position.z);
				return true;
			}
			else
			{
				Vector3 vvx = (v - mule.transform.position).normalized * mule.Speed;
				mule.Body.velocity = new Vector3(vvx.x, 0, vvx.z);
				return false;
			}
		}

		public override void Abort()
		{

		}

		#endregion
	}
}
