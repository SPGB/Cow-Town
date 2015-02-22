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
		float inc = (GameControl.control.pauseMenu)? 100 : 0; 
		GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
		bool hapDif = (GameControl.control.happiness < GameControl.control.happinessLose);
		if (GameControl.control.happiness < 0.1f){
			GUI.Label(new Rect(10 + inc, 10 + inc, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), style);
		} else if (hapDif){
			GUI.Label(new Rect(10 + inc, 10 + inc, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString() + " (-" + GameControl.control.happiness.ToString("F1") + "/5s)", style);
		} else {
			GUI.Label(new Rect(10 + inc, 10 + inc, 100, 100), "Happiness:\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString() + " (-" + GameControl.control.happinessLose.ToString("F1") + "/5s)", style);
		}
		GUI.Label(new Rect(10 + inc, 40 + inc, 100, 100), "Level:\t\t\t\t\t\t" + GameControl.control.level.ToString("F2"), style);
		GUI.Label(new Rect(10 + inc, 70 + inc, 100, 100), "Experience:\t\t\t" + GameControl.control.exp.ToString(), style);
		float troughExp = GameControl.control.troughCurExp;
		if (troughExp >= 30.0f){
			int troughHours = (int)Mathf.Floor((troughExp * 2) / 60);
			int troughMinutes = (int)((troughExp * 2) - (60 * troughHours));
			if (troughMinutes > 0){
				GUI.Label(new Rect(10 + inc, 100 + inc, 100, 100), "Trough:\t\t\t\t\t" + GameControl.control.troughCurExp.ToString() + " / " + GameControl.control.troughMaxExp.ToString() + " (" + troughHours.ToString("F0") + " hours and " + troughMinutes.ToString("F0") + " minutes)", style);
			} else {
				GUI.Label(new Rect(10 + inc, 100 + inc, 100, 100), "Trough:\t\t\t\t\t" + GameControl.control.troughCurExp.ToString() + " / " + GameControl.control.troughMaxExp.ToString() + " (" + troughHours.ToString("F0") + " hours)", style);
			}
		} else {
			int troughMinutes = (int)(troughExp * 2);
			GUI.Label(new Rect(10 + inc, 100 + inc, 100, 100), "Trough:\t\t\t\t\t" + GameControl.control.troughCurExp.ToString() + " / " + GameControl.control.troughMaxExp.ToString() + " (" + troughMinutes.ToString("F0") + " minutes)", style);
		}
		
		if (GameControl.control.pauseMenu){
			GUI.Label(new Rect(10 + inc, 200 + inc, 100, 100), "Strength:\t\t\t\t" + GameControl.control.cowStrength.ToString(), style);
			GUI.Label(new Rect(10 + inc, 230 + inc, 100, 100), "Constitution:\t\t\t" + GameControl.control.cowConstitution.ToString(), style);
			GUI.Label(new Rect(10 + inc, 260 + inc, 100, 100), "Intelligence:\t\t\t" + GameControl.control.cowIntelligence.ToString(), style);
		}
		GUI.EndGroup();
	}
}
