using UnityEngine;
using System.Collections;

public class DisplayGUI : MonoBehaviour {

	//private GUIText happiness;
	//private GUIText level;
	//private GUIText exp;
	//private GUIText trough;
	
	//private GUIText strength;
	//private GUIText constitution;
	//private GUIText intelligence;
	
	//private GUIText levelTest;
	//private GUIText expTest;
	
	public Texture deleteSaveButton;

	void Start () {
		//happiness = transform.Find("happinessHolder").Find("happinessValue").GetComponent<GUIText>();
		//level = transform.Find("levelHolder").Find("levelValue").GetComponent<GUIText>();
		//exp = transform.Find("expHolder").Find("expValue").GetComponent<GUIText>();
		//trough = transform.Find("troughHolder").Find("troughValue").GetComponent<GUIText>();
		
		//strength = transform.Find("cowStats").Find("strengthValue").GetComponent<GUIText>();
		//constitution = transform.Find("cowStats").Find("constitutionValue").GetComponent<GUIText>();
		//intelligence = transform.Find("cowStats").Find("intelligenceValue").GetComponent<GUIText>();
		
		//levelTest = transform.Find("levelTest").GetComponent<GUIText>();
		//expTest = transform.Find("expTest").GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (!GameControl.control.pause) {
			GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
			GUI.Label(new Rect(10, 20, 100, 100), "Cow Town - v" + GameControl.control.version, GameControl.control.text);
			GUI.Label(new Rect(10, 40, 100, 100), "Lv. " + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.text);
			#if !UNITY_EDITOR
			GUI.Label(new Rect(10, 60, 100, 100), "Age. " + GameControl.control.cowAge.TotalDays.ToString("F0") + "." + ((GameControl.control.cowAge.Hours * 10) / 24).ToString("F0") + " days", GameControl.control.text);
			#else
			GUI.Label(new Rect(10, 60, 100, 100), "Age. " + GameControl.control.cowAge.TotalDays.ToString("F0") + "." + ((GameControl.control.cowAge.Hours * 10) / 24).ToString("F0") + ((GameControl.control.cowAge.Minutes * 10) / 60).ToString("F0") + " days", GameControl.control.text);
			#endif
			GUI.Label(new Rect(Screen.width - 130, 20, 100, 100), "Delete Save?", GameControl.control.text);
			if (GUI.Button(new Rect(Screen.width - 100, 45, 90, 30), deleteSaveButton, GUIStyle.none)){
				GameControl.control.Delete();
			}
			
			GUI.EndGroup();
			return;
		}
	}
}
