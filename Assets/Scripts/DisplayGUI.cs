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
	
	private GUIStyle style;
	
	//private GUIText levelTest;
	//private GUIText expTest;

	void Start () {
		style = new GUIStyle();
		style.fontSize = 20;
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
			GUI.Label(new Rect(10, 20, 100, 100), "Cow Town - v" + GameControl.control.version, style);
			GUI.Label(new Rect(10, 40, 100, 100), "Lv" + Mathf.Floor(GameControl.control.level).ToString(), style);
			GUI.EndGroup();
			return;
		}
		float inc = (GameControl.control.pause)? 100 : 0; 
		GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
		bool hapDif = (GameControl.control.happiness < GameControl.control.happinessLose);
		if (GameControl.control.happiness < 0.1f){
			GUI.Label(new Rect(10 + inc, 10 + inc, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), style);
		} else if (hapDif){
			GUI.Label(new Rect(10 + inc, 10 + inc, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString() + " (-" + GameControl.control.happiness.ToString("F1") + "/5s)", style);
		} else {
			GUI.Label(new Rect(10 + inc, 10 + inc, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString() + " (-" + GameControl.control.happinessLose.ToString("F1") + "/5s)", style);
		}

		GUI.Label(new Rect(10 + inc, 70 + inc, 100, 100), "Experience:\t\t\t" + GameControl.control.exp.ToString(), style);
		Trough trough_obj = GameControl.control.trough;
		if (trough_obj) {
			if (trough_obj.get_exp() >= 30.0f) {
				int troughHours = (int)Mathf.Floor ((trough_obj.get_exp() * 2) / 60);
				int troughMinutes = (int)((trough_obj.get_exp() * 2) - (60 * troughHours));
				if (troughMinutes > 0) {
					GUI.Label (new Rect (10 + inc, 100 + inc, 100, 100), "Trough:\t\t\t\t\t" + trough_obj.get_exp ().ToString () + " / " + trough_obj.get_max_exp().ToString () + " (" + troughHours.ToString ("F0") + " hours and " + troughMinutes.ToString ("F0") + " minutes)", style);
				} else {
					GUI.Label (new Rect (10 + inc, 100 + inc, 100, 100), "Trough:\t\t\t\t\t" + trough_obj.get_exp ().ToString () + " / " + trough_obj.get_max_exp().ToString () + " (" + troughHours.ToString ("F0") + " hours)", style);
				}
			} else {
				int troughMinutes = (int)(trough_obj.get_exp() * 2);
				GUI.Label (new Rect (10 + inc, 100 + inc, 100, 100), "Trough:\t\t\t\t\t" + trough_obj.get_exp ().ToString () + " / " + GameControl.control.trough.get_max_exp ().ToString () + " (" + troughMinutes.ToString ("F0") + " minutes)", style);
			}
		}
		GUI.Label(new Rect(10 + inc, 200 + inc, 100, 100), "Strength:\t\t\t\t" + GameControl.control.cowStrength.ToString(), style);
		GUI.Label(new Rect(10 + inc, 230 + inc, 100, 100), "Constitution:\t\t\t" + GameControl.control.cowConstitution.ToString(), style);
		GUI.Label(new Rect(10 + inc, 260 + inc, 100, 100), "Intelligence:\t\t\t" + GameControl.control.cowIntelligence.ToString(), style);

		GUI.EndGroup();
	}
}
