﻿using UnityEngine;
using System.Collections;

public class CowClick : MonoBehaviour {

	public Texture menu;
	private bool gui = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown () {
		if (!GameControl.control.pauseMenu){
			GameControl.control.pauseMenu = true;
			Time.timeScale = 0.0f;
		}
	}
	
	void OnGUI () {
		if (GameControl.control.pauseMenu){
			float width = Screen.width - 100;
			float height = Screen.height - 100;
			// Create one Group to contain both images
			// Adjust the first 2 coordinates to place it somewhere else on-screen
			GUI.BeginGroup(new Rect (50, 50, width, height)); // left, top, width, height
				// Draw the background image
				GUI.DrawTexture(new Rect (0, 0, width, height), menu);
			// End both Groups
			GUI.EndGroup ();
		}
	}
}