using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour {
	
	private float expBarLength = 0.0f;
	private float expBarMaxLength = 2.5f;
	
	//private Vector3 startPos;

	// Use this for initialization
	void Start () {
		expBarLength = expBarMaxLength * (GameControl.control.exp / GameControl.control.expReqToLevel);
		gameObject.transform.localScale = new Vector3(expBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		//startPos = gameObject.transform.position;
		//startPos.x -= 0.5f;
		//gameObject.transform.position = new Vector3(startPos.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.exp != GameControl.control.expExpected){//expBar.transform.x != expBarExpectedLength){
			if (GameControl.control.exp >= GameControl.control.expReqToLevel){
				GameControl.control.exp -= GameControl.control.expReqToLevel;
				GameControl.control.level++;
				GameControl.control.expReqToLevel = Mathf.Floor(GameControl.control.expReqToLevel * 1.5f);
				expBarLength = 0.0f;
				//gameObject.transform.position = startPos;
			}
			expBarLength = expBarMaxLength * (GameControl.control.exp / GameControl.control.expReqToLevel);
			GameControl.control.expExpected = GameControl.control.exp;
			//gameObject.transform.position = new Vector3(startPos.x + ((expBarMaxLength * (exp / expReqToLevel)) / 2), gameObject.transform.position.y, gameObject.transform.position.z);
			gameObject.transform.localScale = new Vector3(expBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		}
	}
}