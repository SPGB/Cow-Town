using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public Texture menu;
	public Texture play;
	public Texture exit;

	// Use this for initialization
	void Start () {
		GameControl.control.pause = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
		float width = Screen.width - 100;
		float height = Screen.height - 100;
		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup(new Rect (50, 50, width, height)); // left, top, width, height
		// Draw the background image
		GUI.DrawTexture(new Rect (0, 0, width, height), menu);
		if (GUI.Button(new Rect((width / 2) - 45, (height / 2) - 30, 90, 30), play, GUIStyle.none)){
			Application.LoadLevel("barn");
			GameControl.control.pause = false;
		}
		if (GUI.Button(new Rect((width / 2) - 45, (height / 2) + 30, 90, 30), exit, GUIStyle.none)){
			Application.Quit();
		}
		// End both Groups
		GUI.EndGroup ();
	}
}
