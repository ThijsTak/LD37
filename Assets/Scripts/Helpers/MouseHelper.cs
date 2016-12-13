using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Helpers
{
	public static class MouseHelper
	{
		// The mouse collision plane.
		private static Plane testPlane = new Plane(Vector3.up, Vector3.zero);

		/// <summary>
		/// Gets the mouse position in world space on the play axis (y=0).
		/// </summary>
		/// <returns></returns>
		public static Vector3 GetMousePosition()
		{
			// Create a ray from the camera to the cursor.
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// Test the distance of the ray against the testplane.
			float distance = 0;
			testPlane.Raycast(ray, out distance);

			// Get the hitpoint of the ray.
			return ray.GetPoint(distance);
		}
	}
}
