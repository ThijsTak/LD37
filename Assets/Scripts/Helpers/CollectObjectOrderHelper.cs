using Assets.Scripts.Behaviours.Mule;
using Behaviours;
using Behaviours.Mule;
using Units;
using UnityEditorInternal;

namespace Helpers
{
	public static class CollectObjectOrderHelper
	{
		public static void CreateOrder(Units.Mule mule, Draggable target, Base home)
		{
			mule.Orders.Enqueue(new GotoPoint(target.transform));
			mule.Orders.Enqueue(new Descend(target.transform.position.y));
			mule.Orders.Enqueue(new GrabCargo(target));
			mule.Orders.Enqueue(new Ascend());
			mule.Orders.Enqueue(new GotoPoint(home.DropPoint));
			mule.Orders.Enqueue(new Descend(home.DropPoint.position.y));
			mule.Orders.Enqueue(new DropCargo());
			mule.Orders.Enqueue(new Ascend());
		}
	}
}
