using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public Texture menu;
	public Texture button_play;
	public Texture button_exit;
	public Texture button_delete;
	public Texture logo;

	public float dip = 110.0f; // Spacing between the middle of the screen and the top of the play button.

	// Use this for initialization
	void Start () {
		GameControl.control.pause = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		/**
		float rx = (float) Screen.width / GameControl.control.native_width;
		float ry = (float) Screen.height / GameControl.control.native_height;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
		*/

		GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		float xPos = Screen.width;
		float yPos = Screen.height;

		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup(new Rect (0, 0, xPos, yPos)); // left, top, width, height
		// Draw the background image
		GUI.DrawTexture(new Rect (0, 0, xPos, yPos), menu);
		GUI.DrawTexture(new Rect ((xPos / 2) - (125 * GameControl.control.screenMulti), 0 * GameControl.control.screenMulti, 300 * GameControl.control.screenMulti, 300 * GameControl.control.screenMulti), logo);
		//GUI.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		//GUI.DrawTexture(new Rect((width / 2) - 10, 0, 20, height), menu);
		//GUI.DrawTexture(new Rect(0, (height / 2) - 10, width, 20), menu);
		//GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if (GUI.Button(new Rect((xPos / 2) - ((GameControl.control.buttonSize.x * GameControl.control.screenMulti) / 2), ((yPos / 2) + (dip * GameControl.control.screenMulti)) - ((GameControl.control.buttonSize.y * GameControl.control.screenMulti) + 10) - ((GameControl.control.buttonSize.y * GameControl.control.screenMulti) / 2), GameControl.control.buttonSize.x * GameControl.control.screenMulti, GameControl.control.buttonSize.y * GameControl.control.screenMulti), button_play, GUIStyle.none)){
			startGame();
		}
		if (GUI.Button(new Rect((xPos / 2) - ((GameControl.control.buttonSize.x * GameControl.control.screenMulti) / 2), ((yPos / 2) + (dip * GameControl.control.screenMulti)) + ((GameControl.control.buttonSize.y * GameControl.control.screenMulti) + 10) - ((GameControl.control.buttonSize.y * GameControl.control.screenMulti) / 2), GameControl.control.buttonSize.x * GameControl.control.screenMulti, GameControl.control.buttonSize.y * GameControl.control.screenMulti), button_exit, GUIStyle.none)){
			endGame();
		}

		// End both Groups
		GUI.EndGroup ();
	}
	
	public void startGame(){
		Application.LoadLevel("barn");
		Debug.Log("LOAD ON TITLE");
		GameControl.control.trough = GameObject.Find("trough").GetComponent<Trough>();
		GameControl.control.cow = GameObject.Find("cow").GetComponent<Cow>();
		GameControl.control.Load();
		GameControl.control.pause = false;
	}
	
	public void endGame(){
		GameControl.control.Save();
		Application.Quit();
	}
}
