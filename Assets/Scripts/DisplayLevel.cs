using UnityEngine;
using System.Collections;

public class DisplayLevel : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		this.guiText.text = "Level: " + GameControl.control.level.ToString();
	}
}
