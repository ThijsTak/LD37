using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using UnityEngine;

namespace Core
{
	public class GlobalManager : MonoBehaviour
	{
		public static GlobalManager Instance;

		public BasicSettings Settings = new BasicSettings();

		public void Start()
		{
			Instance = this;
		}
	}
}
