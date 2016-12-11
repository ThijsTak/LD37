using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class MinMaxValue
	{
		/// <summary>
		/// The current value.
		/// </summary>
		[SerializeField]
		private float current;

		[SerializeField]
		private float max;

		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		public float Current
		{
			get { return current; }
			set { current = Mathf.Clamp(value, 0, Max); }
		}

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		public float Max
		{
			get { return max; }
			set
			{
				max = value;
				if (current > max)
				{
					current = max;
				} 
			}
		}

		/// <summary>
		/// Changes the value.
		/// </summary>
		/// <param name="delta">The delta.</param>
		/// <returns>Any leftover value due to hitting ceiling of floor.</returns>
		public float ChangeValue(float delta)
		{
			// Check if the current value will lower our value below 0.
			if (current + delta < 0)
			{
				var ret = delta - current;
				current = 0;
				return ret;
			}

			if (current + delta > Max)
			{
				var ret = Max - current;
				current = Max;
				return ret;
			}

			current += delta;
			return 0;
		}
	}
}
