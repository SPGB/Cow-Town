using UnityEngine;
using System.Collections;

public class DisplayGUI : MonoBehaviour {

	private GUIText happiness;
	private GUIText level;
	private GUIText exp;
	private GUIText trough;
	
	private GUIText strength;
	private GUIText constitution;
	private GUIText intelligence;

	void Start () {
		happiness = transform.Find("happinessHolder").Find("happinessValue").GetComponent<GUIText>();
		level = transform.Find("levelHolder").Find("levelValue").GetComponent<GUIText>();
		exp = transform.Find("expHolder").Find("expValue").GetComponent<GUIText>();
		trough = transform.Find("troughHolder").Find("troughValue").GetComponent<GUIText>();
		
		strength = transform.Find("cowStats").Find("strengthValue").GetComponent<GUIText>();
		constitution = transform.Find("cowStats").Find("constitutionValue").GetComponent<GUIText>();
		intelligence = transform.Find("cowStats").Find("intelligenceValue").GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		happiness.text = GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString() + " (-" + GameControl.control.happinessLose.ToString("F1") + "/s)";
		level.text = GameControl.control.level.ToString();
		exp.text = GameControl.control.exp.ToString() + " / " + GameControl.control.expReqToLevel.ToString();
		float troughExp = GameControl.control.troughCurExp;
		if (troughExp >= 30.0f){
			int troughHours = (int)Mathf.Floor((troughExp * 2) / 60);
			int troughMinutes = (int)((troughExp * 2) - (60 * troughHours));
			if (troughMinutes > 0){
				trough.text = GameControl.control.troughCurExp.ToString() + " / " + GameControl.control.troughMaxExp.ToString() + " (" + troughHours.ToString("F0") + " hours and " + troughMinutes.ToString("F0") + " minutes)";
			} else {
				trough.text = GameControl.control.troughCurExp.ToString() + " / " + GameControl.control.troughMaxExp.ToString() + " (" + troughHours.ToString("F0") + " hours)";
			}
		} else {
			int troughMinutes = (int)(troughExp * 2);
			trough.text = GameControl.control.troughCurExp.ToString() + " / " + GameControl.control.troughMaxExp.ToString() + " (" + troughMinutes.ToString("F0") + " minutes)";
		}
		
		strength.text = GameControl.control.cowStrength.ToString();
		constitution.text = GameControl.control.cowConstitution.ToString();
		intelligence.text = GameControl.control.cowIntelligence.ToString();
	}
}
