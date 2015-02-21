using UnityEngine;
using System;
using System.Collections;

public class Happiness : MonoBehaviour {

	public Material semi;
	public Material full;

	private float hapBarLength = 0.0f;
	private float hapBarMaxLength = 2.5f;
	
	private DateTime hapTime1;
	private DateTime hapTime2;
	private TimeSpan hapTimeSpan;
	
	// Use this for initialization
	void Start () {
		hapTime1 = DateTime.Now;
		hapBarLength = hapBarMaxLength * (GameControl.control.happiness / GameControl.control.happinessMax);
		gameObject.transform.localScale = new Vector3(hapBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.happiness != GameControl.control.happinessExpected){//expBar.transform.x != expBarExpectedLength){
			if (GameControl.control.happiness >= GameControl.control.happinessMax){
				GameControl.control.happiness = GameControl.control.happinessMax;
				renderer.material = full;
			} else {
				renderer.material = semi;
			}
			hapBarLength = hapBarMaxLength * (GameControl.control.happiness / GameControl.control.happinessMax);
			GameControl.control.happinessExpected = GameControl.control.happiness;
			//gameObject.transform.position = new Vector3(startPos.x + ((expBarMaxLength * (exp / expReqToLevel)) / 2), gameObject.transform.position.y, gameObject.transform.position.z);
			gameObject.transform.localScale = new Vector3(hapBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		}
		
		hapTime2 = DateTime.Now;
		hapTimeSpan = hapTime2 - hapTime1;
		if ((int)hapTimeSpan.TotalSeconds >= 5){
			GameControl.control.happiness -= GameControl.control.happinessLose;
			hapTime1 = DateTime.Now;
		}
	}
}
