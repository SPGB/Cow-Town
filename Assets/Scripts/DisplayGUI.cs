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
			GUI.Label(new Rect(10 * GameControl.control.screenMulti, (20 * GameControl.control.screenMulti), 100, 100), "Lvl " + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.cowText);
		} else {
			GUI.Label(new Rect(10 * GameControl.control.screenMulti, (20 * GameControl.control.screenMulti), 100, 100), "Lvl " + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.text);
		}

		GUI.DrawTexture(new Rect(Screen.width - (100 * GameControl.control.screenMulti), 5 * GameControl.control.screenMulti, 90 * GameControl.control.screenMulti, 45 * GameControl.control.screenMulti), moneyBackground);
		GUI.Label(new Rect(Screen.width - (80 * GameControl.control.screenMulti), (7 * GameControl.control.screenMulti), 100, 100), "" + GameControl.control.money, GameControl.control.moneyText);
		GUI.EndGroup();
		return;
		//}
	}
}
