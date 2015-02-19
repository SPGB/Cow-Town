using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour {
	
	private float expBarLength = 0.0f;
	private float expBarMaxLength = 2.5f;
	
	private float barMulti;
	
	//private Vector3 startPos;

	// Use this for initialization
	void Start () {
		barMulti = GameControl.control.level - (Mathf.Floor(GameControl.control.level));
		expBarLength = expBarMaxLength * barMulti;
		gameObject.transform.localScale = new Vector3(expBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		//startPos = gameObject.transform.position;
		//startPos.x -= 0.5f;
		//gameObject.transform.position = new Vector3(startPos.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.exp != GameControl.control.expExpected){//expBar.transform.x != expBarExpectedLength){
			GameControl.control.level = (100 * (GameControl.control.exp / (GameControl.control.exp + 1000)));
			barMulti = GameControl.control.level - (Mathf.Floor(GameControl.control.level));
			expBarLength = expBarMaxLength * barMulti;
			//gameObject.transform.position = new Vector3(startPos.x + ((expBarMaxLength * (exp / expReqToLevel)) / 2), gameObject.transform.position.y, gameObject.transform.position.z);
			gameObject.transform.localScale = new Vector3(expBarLength, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			GameControl.control.expExpected = GameControl.control.exp;
		}
	}
}