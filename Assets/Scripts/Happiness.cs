using UnityEngine;
using System;
using System.Collections;

public class Happiness : MonoBehaviour {

	//public Material semi;
	//public Material full;

	private float hapBarLength = 0.0f;
	private float hapBarMaxLength = 215f;
	
	private DateTime hapTime1;
	private DateTime hapTime2;
	private TimeSpan hapTimeSpan;

	public Texture background_texture; //the backing
	public Texture foreground_texture; //the blue bar
	private float position_x = 272.8f; //pos from bottom

	// Use this for initialization
	void Start () {
		hapTime1 = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.happiness >= GameControl.control.happinessMax){
			GameControl.control.happiness = GameControl.control.happinessMax;
		}
		hapBarLength = hapBarMaxLength * (GameControl.control.happiness / GameControl.control.happinessMax);
		
		if (!GameControl.control.pause){
			hapTime2 = DateTime.Now;
			hapTimeSpan = hapTime2 - hapTime1;
			if ((int)hapTimeSpan.TotalSeconds >= 5){
				GameControl.control.happiness -= GameControl.control.happinessLose;
				hapTime1 = DateTime.Now;
			}
		}
	}
	
	void OnGUI () {
		if (!GameControl.control.pause){
			// Create one Group to contain both images
			// Adjust the first 2 coordinates to place it somewhere else on-screen
			GUI.BeginGroup (new Rect ( (Screen.width - hapBarMaxLength ) / 2,Screen.height - position_x, hapBarMaxLength,10));
				// Draw the background image
				GUI.DrawTexture (new Rect (0,0, hapBarMaxLength,10), background_texture);
				// Create a second Group which will be clipped
				// We want to clip the image and not scale it, which is why we need the second Group
				GUI.BeginGroup (new Rect (0,0, hapBarLength, 10));
					// Draw the foreground image
					GUI.DrawTexture (new Rect (0,0,hapBarLength,10), foreground_texture);
				// End both Groups
				GUI.EndGroup ();
			GUI.EndGroup ();
		}
	}
}