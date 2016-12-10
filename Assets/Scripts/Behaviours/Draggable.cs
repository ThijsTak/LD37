using UnityEngine;

namespace Behaviours
{
	public class Draggable : MonoBehaviour
	{
		public Rigidbody body;

		public void Start()
		{
			body = GetComponent<Rigidbody>();
		}
	}
}
