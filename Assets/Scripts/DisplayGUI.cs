using UnityEngine;
using System.Collections;

public class DisplayGUI : MonoBehaviour {	
	public Texture moneyBackground;

	void Start () {
	}

	void OnGUI () {

		//if (!GameControl.control.pause) {
		GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
		if (GameControl.control.pause) {
			GUI.Label(new Rect(10, (20 * GameControl.control.screenMulti), 100, 100), "Lvl " + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.cowText);
		} else {
			GUI.Label(new Rect(10, (20 * GameControl.control.screenMulti), 100, 100), "Lvl " + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.text);
		}

		GameControl.control.cowText.fontSize = 28;
		GameControl.control.cowText.fontStyle = FontStyle.Bold;

		GUI.DrawTexture(new Rect(Screen.width - (130 * GameControl.control.screenMulti), 5 * GameControl.control.screenMulti, 90, 45), moneyBackground);
		GUI.Label(new Rect(Screen.width - (110 * GameControl.control.screenMulti), (10 * GameControl.control.screenMulti), 100, 100), "" + GameControl.control.money, GameControl.control.cowText);
		GUI.EndGroup();
		return;
		//}
	}
}
