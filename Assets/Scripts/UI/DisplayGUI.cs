using UnityEngine;
using System.Collections;

public class DisplayGUI : MonoBehaviour {	
	public Texture moneyBackground;

	void Start () {

	}

	void OnGUI () {
		GameControl.control.moneyText.normal.textColor = Color.black;
		GUI.BeginGroup(new Rect (-5f, 0, Screen.width, Screen.height)); // left, top, width, height

		GUI.DrawTexture(new Rect(Screen.width - (100 * GameControl.control.screenMulti), 5 * GameControl.control.screenMulti, 90 * GameControl.control.screenMulti, 45 * GameControl.control.screenMulti), moneyBackground);
		GUI.Label(new Rect(Screen.width - (80 * GameControl.control.screenMulti), (7 * GameControl.control.screenMulti), 100, 100), "" + GameControl.control.money, GameControl.control.moneyText);

		GUI.EndGroup();
	}
}
