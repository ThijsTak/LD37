using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Behaviours.Mule;
using Core;
using UnityEngine;

namespace Behaviours.Mule
{
	class Descend : BehaviourState
	{
		public Descend()
		{
			
		}

		public Descend(float descendHeight)
		{
			DescendHeight = descendHeight;
		}

		public float DescendHeight = 0.0f;

		#region Overrides of BehaviourState

		public override bool Update(Units.Mule mule)
		{
			// Move towards the target.
			float dir = mule.transform.position.y - DescendHeight;

			// Are we there yet?
			if (dir <= GlobalManager.Instance.Settings.MuleDescentSpeed * Time.fixedDeltaTime)
			{
				mule.Body.velocity = Vector3.zero;
				mule.transform.position = new Vector3(mule.transform.position.x, 
					DescendHeight, mule.transform.position.z);

				return true;
			}
			else
			{
				mule.Body.velocity = new Vector3(0, -GlobalManager.Instance.Settings.MuleDescentSpeed, 0);
				return false;
			}
		}

		public override void Abort()
		{
			
		}

		#endregion
	}
}
