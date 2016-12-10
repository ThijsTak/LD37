using Behaviours;
using Core;
using UnityEngine;

namespace Behaviours.Mule
{
	public class GrabCargo : BehaviourState
	{
		/// <summary>
		/// The target to pick up.
		/// </summary>
		private Draggable Target = null;

		#region Overrides of BehaviourState

		public override bool Update(Units.Mule mule)
		{
			mule.Cargo = Target;
			Target.body.useGravity = false;
			return true;
		}

		public override void Abort()
		{
			// Remove the linked item and readd it to the queue.
		}

		#endregion
	}
}
