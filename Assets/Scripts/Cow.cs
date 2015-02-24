using UnityEngine;
using System;
using System.Collections;

public class Cow : MonoBehaviour {

	public Texture menu;
	public Texture closeButton;
	
	public DateTime born;
	public DateTime now;
	public TimeSpan age;

	// Use this for initialization
	void Start () {
		GameControl.control.cow = this;
		GameControl.control.Load();
	}
	
	// Update is called once per frame
	void Update () {
		if (born != GameControl.control.cowBorn) born = GameControl.control.cowBorn;
		now = DateTime.Now;
		age = now - born;
		GameControl.control.cowAge = age;
	}
	
	void OnMouseDown () {
		if (!GameControl.control.pause){
			GameControl.control.pause = true;
		}
	}
	
	void OnGUI () {
		if (GameControl.control.pause){
			GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
			float width = Screen.width - 100;
			float height = Screen.height - 100;
			// Create one Group to contain both images
			// Adjust the first 2 coordinates to place it somewhere else on-screen
			GUI.BeginGroup(new Rect (50, 50, width, height)); // left, top, width, height
				// Draw the background image
				GUI.DrawTexture(new Rect (0, 0, width, height), menu);
				if (GUI.Button(new Rect(width - 130, 10, 90, 30), closeButton, GUIStyle.none)){
					GameControl.control.pause = false;
				}
			// End both Groups
			GUI.EndGroup ();
		}
	}
}