using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour {

	//public float minBarLen = 0.0f;
	//public float maxBarLen = 1.0f;
	public float exp = 0.0f;
	public float expReqToLevel = 10.0f;
	public int level = 1;
	
	public float expExpected = 0.0f;
	
	private float expBarLength = 0.0f;
	private float expBarMaxLength = 2.5f;
	
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		
		gameObject.transform.localScale = new Vector3(expBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		startPos = gameObject.transform.position;
		//startPos.x -= 0.5f;
		gameObject.transform.position = new Vector3(startPos.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (exp != expExpected){//expBar.transform.x != expBarExpectedLength){
			if (exp >= expReqToLevel){
				exp -= expReqToLevel;
				level++;
				expReqToLevel = Mathf.Floor(expReqToLevel * 1.5f);
				expBarLength = 0.0f;
				gameObject.transform.position = startPos;
			}
			expBarLength = expBarMaxLength * (exp / expReqToLevel);
			expExpected = exp;
			//gameObject.transform.position = new Vector3(startPos.x + ((expBarMaxLength * (exp / expReqToLevel)) / 2), gameObject.transform.position.y, gameObject.transform.position.z);
			gameObject.transform.localScale = new Vector3(expBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		}
	}
	
	public void addExp(int value){
		exp += value;
	}
	public float getExpCurrent(){
		return exp;
	}
	public float getExpNeeded(){
		return expReqToLevel;
	}
	public int getLevel(){
		return level;
	}
}
