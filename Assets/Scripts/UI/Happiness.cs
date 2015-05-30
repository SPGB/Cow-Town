using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Happiness : MonoBehaviour {
	private DateTime hapTime1;
	private DateTime hapTime2;
	private TimeSpan hapTimeSpan;
	private float rate = 5.0f;

	// Use this for initialization
	void Start () {
		hapTime1 = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.happiness >= GameControl.control.happinessMax){
			GameControl.control.happiness = GameControl.control.happinessMax;
		}
		if (!GameControl.control.pause){
			hapTime2 = DateTime.Now;
			hapTimeSpan = hapTime2 - hapTime1;
			//if ((int)hapTimeSpan.TotalSeconds >= GameControl.control.happinessDecRate){
			if ((int)hapTimeSpan.TotalSeconds >= rate){
				GameControl.control.happiness -= GameControl.control.happinessLose;
				hapTime1 = DateTime.Now;
			}
		}
		this.GetComponent<Image>().fillAmount = GameControl.control.happiness / GameControl.control.happinessMax;
	}

	public float getRate(){
		return rate;
	}
	public void setRate(float rate){
		this.rate = rate;
	}
}