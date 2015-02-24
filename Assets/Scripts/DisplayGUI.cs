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
			GUI.Label(new Rect(10, 40, 100, 100), "Lv" + Mathf.Floor(GameControl.control.level).ToString(), GameControl.control.text);
			GUI.EndGroup();
			return;
		}
		GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
		bool hapDif = (GameControl.control.happiness < GameControl.control.happinessLose);
		Trough trough_obj = GameControl.control.trough;
		
		if (GameControl.control.pause){
			if (GameControl.control.happiness < 0.1f){
				GUI.Label(new Rect(110, 110, 100, 100), "Happiness:", GameControl.control.text);
				GUI.Label(new Rect(110, 110, 100, 100), "\t\t\t\t\t\t\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
			} else if (hapDif){
				GUI.Label(new Rect(110, 110, 100, 100), "Happiness:", GameControl.control.text);
				GUI.Label(new Rect(110, 100, 100, 100), "\t\t\t\t\t\t\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
				GUI.Label(new Rect(110, 120, 100, 100), "\t\t\t\t\t\t\t\t\t(-" + GameControl.control.happiness.ToString("F1") + "/5s)", GameControl.control.text);
			} else {
				GUI.Label(new Rect(110, 110, 100, 100), "Happiness:", GameControl.control.text);
				GUI.Label(new Rect(110, 100, 100, 100), "\t\t\t\t\t\t\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
				GUI.Label(new Rect(110, 120, 100, 100), "\t\t\t\t\t\t\t\t\t(-" + GameControl.control.happinessLose.ToString("F1") + "/5s)", GameControl.control.text);
			}
			
			GUI.Label(new Rect(110, 150, 100, 100), "Experience:\t\t\t" + GameControl.control.exp.ToString(), GameControl.control.text);
			
			if (trough_obj.get_exp() >= 30.0f){
				int troughHours = (int)Mathf.Floor((trough_obj.get_exp() * 2) / 60);
				int troughMinutes = (int)((trough_obj.get_exp() * 2) - (60 * troughHours));
				if (troughMinutes > 0){
					GUI.Label(new Rect(110, 200, 100, 100), "Trough:", GameControl.control.text);
					GUI.Label(new Rect(110, 180, 100, 100), "\t\t\t\t\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString(), GameControl.control.text);
					GUI.Label(new Rect(110, 200, 100, 100), "\t\t\t\t\t\t\t\t\t(" + troughHours.ToString("F0") + " hours and", GameControl.control.text);
					GUI.Label(new Rect(110, 220, 100, 100), "\t\t\t\t\t\t\t\t\t" + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
				} else {
					GUI.Label(new Rect(110, 200, 100, 100), "Trough:", GameControl.control.text);
					GUI.Label(new Rect(110, 190, 100, 100), "\t\t\t\t\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString(), GameControl.control.text);
					GUI.Label(new Rect(110, 210, 100, 100), "\t\t\t\t\t\t\t\t\t(" + troughHours.ToString("F0") + " hours)", GameControl.control.text);
				}
			} else {
				int troughMinutes = (int)(trough_obj.get_exp() * 2);
				GUI.Label(new Rect(110, 200, 100, 100), "Trough:", GameControl.control.text);
				GUI.Label(new Rect(110, 190, 100, 100), "\t\t\t\t\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString(), GameControl.control.text);
				GUI.Label(new Rect(110, 210, 100, 100), "\t\t\t\t\t\t\t\t\t(" + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
			}
			
			GUI.Label(new Rect(110, 260, 100, 100), "Age:\t\t\t\t\t\t\t" + GameControl.control.cowAge.TotalDays.ToString("F0") + "." + ((GameControl.control.cowAge.TotalHours * 10) / 24).ToString("F0") + " days", GameControl.control.text);
			
			GUI.Label(new Rect(110, 330, 100, 100), "Strength:\t\t\t\t" + GameControl.control.cowStrength.ToString(), GameControl.control.text);
			GUI.Label(new Rect(110, 360, 100, 100), "Constitution:\t\t\t" + GameControl.control.cowConstitution.ToString(), GameControl.control.text);
			GUI.Label(new Rect(110, 390, 100, 100), "Intelligence:\t\t\t" + GameControl.control.cowIntelligence.ToString(), GameControl.control.text);
		}/* else {
			if (GameControl.control.happiness < 0.1f){
				GUI.Label(new Rect(10, 10, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
			} else if (hapDif){
				GUI.Label(new Rect(10, 10, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString() + " (-" + GameControl.control.happiness.ToString("F1") + "/5s)", GameControl.control.text);
			} else {
				GUI.Label(new Rect(10, 10, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString() + " (-" + GameControl.control.happinessLose.ToString("F1") + "/5s)", GameControl.control.text);
			}
			
			GUI.Label(new Rect(10, 40, 100, 100), "Experience:\t\t\t" + GameControl.control.exp.ToString(), GameControl.control.text);
			
			if (trough_obj) {
				if (trough_obj.get_exp() >= 30.0f){
					int troughHours = (int)Mathf.Floor((trough_obj.get_exp() * 2) / 60);
					int troughMinutes = (int)((trough_obj.get_exp() * 2) - (60 * troughHours));
					if (troughMinutes > 0){
						GUI.Label(new Rect(10, 70, 100, 100), "Trough:\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString() + " (" + troughHours.ToString("F0") + " hours and " + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
					} else {
						GUI.Label(new Rect(10, 70, 100, 100), "Trough:\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString() + " (" + troughHours.ToString("F0") + " hours)", GameControl.control.text);
					}
				} else {
					int troughMinutes = (int)(trough_obj.get_exp() * 2);
					GUI.Label(new Rect(10, 70, 100, 100), "Trough:\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString() + " (" + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
				}
			}
		}
		**/
		GUI.EndGroup();
	}
}
