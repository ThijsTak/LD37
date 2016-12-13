using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
	public static class HeightHelper
	{
		public static float GetHeightFromTerrain(Vector3 position)
		{
			// Set height.
			var colliders = Physics.RaycastAll(new Ray(
				new Vector3(position.x, 10000, position.z), Vector3.down), 10000);
			foreach (RaycastHit raycastHit in colliders)
			{
				if (raycastHit.collider.gameObject.tag == "Terrain")
				{
					return raycastHit.point.y;
				}
			}

			return 0;
		}

		public static Vector3 HeightCorrect(Vector3 point)
		{
			return new Vector3(point.x, GetHeightFromTerrain(new Vector3(point.x, 10000, point.z)), point.z);
		}
	}
}
