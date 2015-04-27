using UnityEngine;
using System.Collections;

public class DisplayGUI : MonoBehaviour {	
	public Texture deleteSaveButton;

	void Start () {
	}

	void OnGUI () {

		if (!GameControl.control.pause) {
			GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
			GUI.Label(new Rect(10, (20 * GameControl.control.screenMulti), 100, 100), "Lvl " + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.text);

			GUI.Label(new Rect(Screen.width - (130 * GameControl.control.screenMulti), (20 * GameControl.control.screenMulti), 100, 100), "$" + GameControl.control.money, GameControl.control.text);			
			GUI.EndGroup();
			return;
		}
	}
}
