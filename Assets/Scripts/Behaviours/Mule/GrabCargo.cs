using Behaviours;
using Core;
using UnityEngine;

namespace Behaviours.Mule
{
	public class GrabCargo : BehaviourState
	{
		public GrabCargo()
		{

		}

		public GrabCargo(Collectable target)
		{
			Target = target;
		}

		/// <summary>
		/// The target to pick up.
		/// </summary>
		private Collectable Target = null;

		#region Overrides of BehaviourState

		public override bool Update(Units.Mule mule)
		{
			mule.Cargo = Target;
			Target.Transporter = mule.gameObject;
			return true;
		}

		public override void Abort()
		{
			// Remove the linked item and readd it to the queue.
		}

		#endregion
	}
}
