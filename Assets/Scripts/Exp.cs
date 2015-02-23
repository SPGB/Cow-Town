﻿using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour { //experience bar for cow
	
	public float expBarLength = 0.0f; //the filled in part of the bar
	private float expBarMaxLength = 215f; //the maximum exp bar length

	public Texture background_texture; //the backing
	public Texture foreground_texture; //the blue bar

	private float position_x = 283; //pos from bottom
	public float barMulti; //the % progress into the level

	void Start () {
		barMulti = GameControl.control.level - (Mathf.Floor(GameControl.control.level));
	}

	void Update () {

		GameControl.control.level = (100 * (GameControl.control.exp / (GameControl.control.exp + 1000)));
		barMulti = GameControl.control.level - (Mathf.Floor(GameControl.control.level));
		expBarLength = expBarMaxLength * barMulti;
	}

	void OnGUI () {
		if (!GameControl.control.pause){
			// Create one Group to contain both images
			// Adjust the first 2 coordinates to place it somewhere else on-screen
			GUI.BeginGroup (new Rect ( (Screen.width - expBarMaxLength ) / 2,Screen.height - position_x, expBarMaxLength,10));
				// Draw the background image
				GUI.DrawTexture (new Rect (0,0, expBarMaxLength,10), background_texture);
				// Create a second Group which will be clipped
				// We want to clip the image and not scale it, which is why we need the second Group
				GUI.BeginGroup (new Rect (0,0, expBarLength, 10));
					// Draw the foreground image
					GUI.DrawTexture (new Rect (0,0,expBarLength,10), foreground_texture);
				// End both Groups
				GUI.EndGroup ();
			GUI.EndGroup ();
		}
	}
}