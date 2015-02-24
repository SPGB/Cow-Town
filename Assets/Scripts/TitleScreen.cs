﻿using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public Texture menu;
	public Texture hay;
	public Texture rock;

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
		if (GUI.Button(new Rect((width / 2) - (hay.width / 2) - 50, (height / 2) - (hay.height / 2) - 100, hay.width, hay.height), hay, GUIStyle.none)){
			startGame();
		}
		if (GUI.Button(new Rect((width / 2) - (hay.width / 2) - 50, (height / 2) - (hay.height / 2) - (100 - 40), width, hay.height), "\t\t\t\t\t\tPLAY", GameControl.control.text)){
			startGame();
		}
		if (GUI.Button(new Rect((width / 2) - (rock.width / 2) - 50, (height / 2) - (rock.height / 2) + 100, rock.width, rock.height), rock, GUIStyle.none)){
			endGame();
		}
		if (GUI.Button(new Rect((width / 2) - (rock.width / 2) - 50, (height / 2) - (rock.height / 2) + (100 + 28), width, rock.height), "\t\t\t\t\t\tEXIT", GameControl.control.text)){
			endGame();
		}
		// End both Groups
		GUI.EndGroup ();
	}
	
	public void startGame(){
		Application.LoadLevel("barn");
		GameControl.control.Load();
		GameControl.control.pause = false;
	}
	
	public void endGame(){
		GameControl.control.Save();
		Application.Quit();
	}
}
