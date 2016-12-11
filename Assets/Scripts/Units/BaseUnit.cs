using UnityEngine;

namespace Units
{
	public class BaseUnit : MonoBehaviour
	{
		/// <summary>
		/// Gets the position translated to the 2D plane.
		/// </summary>
		public Vector2 Position
		{
			get
			{
				return new Vector2(transform.position.x, transform.position.z);
			}
		}


		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public virtual void DrainEnergy(float energy)
		{
			
		}

		public virtual float GetEnergy()
		{
			return 0;
		}
	}
}
