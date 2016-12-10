using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Behaviours.Mule;
using Core;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Mule
{
	public class Ascend : BehaviourState
	{
		#region Overrides of BehaviourState

		public override bool Update(Units.Mule mule)
		{
			// Move towards the target.
			// float dir = mule.transform.position.y - GlobalManager.Instance.Settings.MuleFlyHeight;

			// Are we there yet?
			if (mule.transform.position.y >= GlobalManager.Instance.Settings.MuleFlyHeight)
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
