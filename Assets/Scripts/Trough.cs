﻿using UnityEngine;
using System.Collections;

public class Trough : MonoBehaviour {

	private GameObject wheel1;
	private GameObject wheel2;
	private GameObject wheel3;
	
	private Vector3 mousePos;
	
	private float amount;

	// Use this for initialization
	void Start () {
		wheel1 = GameObject.Find("wheel1");
		wheel2 = GameObject.Find("wheel2");
		wheel3 = GameObject.Find("wheel3");
	}
	
	// Update is called once per frame
	void Update () {
		wheel1.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
		wheel2.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
		wheel3.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
	}
	
	void OnMouseDrag () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		amount = transform.position.x - mousePos.x;
		transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
	}
	
	void OnMouseUp () {
		amount = 0.0f;
	}
}
