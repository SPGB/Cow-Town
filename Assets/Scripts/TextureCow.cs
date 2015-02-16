using UnityEngine;
using System.Collections;

public class TextureCow : MonoBehaviour {

	public Sprite cowTex0;
	public Sprite cowTex1;
	public Sprite cowTex2;
	public Sprite cowTex3;
	
	public Sprite cowTex;
	
	private int levelExpected = 0;

	// Use this for initialization
	void Start () {
		cowTex = cowTex0;
		if (GameControl.control.level >= 5 && GameControl.control.level < 10){
			cowTex = cowTex1;
		} else if (GameControl.control.level >= 10 && GameControl.control.level < 15){
			cowTex = cowTex2;
		} else if (GameControl.control.level >= 15){
			cowTex = cowTex3;
		}
		gameObject.GetComponent<SpriteRenderer>().sprite = cowTex;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.level != levelExpected){
			if (GameControl.control.level >= 5 && GameControl.control.level < 10){
				cowTex = cowTex1;
			} else if (GameControl.control.level >= 10 && GameControl.control.level < 15){
				cowTex = cowTex2;
			} else if (GameControl.control.level >= 15){
				cowTex = cowTex3;
			}
			gameObject.GetComponent<SpriteRenderer>().sprite = cowTex;
			levelExpected = GameControl.control.level;
		}
	}
}