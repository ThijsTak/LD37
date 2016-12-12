﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour {

	void Start()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}

	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.green;
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
}