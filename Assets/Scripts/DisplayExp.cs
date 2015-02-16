using UnityEngine;
using System.Collections;

public class DisplayExp : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		this.guiText.text = "Exp: " + GameControl.control.exp.ToString() + " / " + GameControl.control.expReqToLevel.ToString();
	}
}
