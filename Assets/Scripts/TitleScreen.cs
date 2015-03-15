using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public Texture menu;
	public Texture button_play;
	public Texture button_exit;
	public Texture button_delete;

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
		//GUI.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		//GUI.DrawTexture(new Rect((width / 2) - 10, 0, 20, height), menu);
		//GUI.DrawTexture(new Rect(0, (height / 2) - 10, width, 20), menu);
		//GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if (GUI.Button(new Rect((width / 2) - (button_play.width / 2), (height / 4) - (button_play.height / 2), button_play.width, button_play.height), button_play, GUIStyle.none)){
			startGame();
		}
		if (GUI.Button(new Rect((width / 2) - (button_delete.width / 2), ((height / 4) + (height / 4)) - (button_delete.height / 2), button_delete.width, button_delete.height), button_delete, GUIStyle.none)){
			GameControl.control.Delete();
		}
		if (GUI.Button(new Rect((width / 2) - (button_exit.width / 2), ((height / 4) + (height / 2)) - (button_exit.height / 2), button_exit.width, button_exit.height), button_exit, GUIStyle.none)){
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
