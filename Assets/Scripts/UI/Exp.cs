using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour { //experience bar for cow
	
	public float expBarLength = 0.0f; //the filled in part of the bar
	private float expBarMaxLength = 215f; //the maximum exp bar length

	public Texture background_texture; //the backing
	public Texture foreground_texture; //the blue bar

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
		if (GameControl.control.pause) return;

		GUI.BeginGroup (new Rect (transform.localPosition.x, transform.localPosition.y, expBarMaxLength * GameControl.control.screenMulti, 10 * GameControl.control.screenMulti));
		GUI.DrawTexture (new Rect (0,0, expBarMaxLength,10), background_texture); // Draw the background image
		//Foreground
		GUI.BeginGroup (new Rect (0,0, expBarLength * GameControl.control.screenMulti, 10 * GameControl.control.screenMulti));
		GUI.DrawTexture (new Rect (0,0, expBarLength, 10), foreground_texture);
		GUI.EndGroup();
		GUI.EndGroup ();

		GUI.BeginGroup (new Rect (transform.localPosition.x + expBarMaxLength - 20f, transform.localPosition.y, expBarMaxLength * GameControl.control.screenMulti, 25 * GameControl.control.screenMulti));
		GUI.Label(new Rect(0, 0, 100, 100), "Lvl " + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.text);
		// End both Groups
		GUI.EndGroup ();
	}
}