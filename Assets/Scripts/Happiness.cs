using UnityEngine;
using System.Collections;

public class Happiness : MonoBehaviour {

	public Material semi;
	public Material full;

	private float hapBarLength = 0.0f;
	private float hapBarMaxLength = 2.5f;
	
	private float timer = 40.0f;
	
	// Use this for initialization
	void Start () {
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
		
		if (timer <= 0.0f && GameControl.control.happiness >= 0.0f){
			GameControl.control.happiness -= GameControl.control.happinessLose;
			timer = 40.0f;
		}
		timer -= 0.1f;
	}
}
