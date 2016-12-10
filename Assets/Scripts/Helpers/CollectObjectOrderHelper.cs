using Behaviours;
using Behaviours.Mule;
using Units;

namespace Helpers
{
	public static class CollectObjectOrderHelper
	{
		public static void CreateOrder(Units.Mule mule, Draggable target)
		{
			mule.Orders.Enqueue(new GotoPoint(target.transform));
		}
	}
}
