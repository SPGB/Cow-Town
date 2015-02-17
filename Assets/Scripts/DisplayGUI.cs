using UnityEngine;
using System.Collections;

public class DisplayGUI : MonoBehaviour {

	private GUIText level;
	private GUIText exp;
	private GUIText trough;

	void Start () {
		level = transform.Find("levelText").GetComponent<GUIText>();
		exp = transform.Find("expText").GetComponent<GUIText>();
		trough = transform.Find("troughText").GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		level.text = "Level: " + GameControl.control.level.ToString();
		exp.text = "Exp: " + GameControl.control.exp.ToString() + " / " + GameControl.control.expReqToLevel.ToString();
		trough.text = "Trough: " + GameControl.control.troughCurExp.ToString() + " / " + GameControl.control.troughMaxExp.ToString();
	}
}
