using UnityEngine;
using System.Collections;

public class TextureCow : MonoBehaviour {

	public Sprite cowTex0;
	public Sprite cowTex1;
	public Sprite cowTex2;
	public Sprite cowTex3;
	
	public Sprite cowTex;
	

	// Use this for initialization
	void Start () {
		cowTex = cowTex0;
		if (GameControl.control.level >= 5.0f){
			cowTex = cowTex1;
		}
		if (GameControl.control.exp >= 500){
			cowTex = cowTex2;
		}
		if (GameControl.control.exp >= 1000){
			cowTex = cowTex3;
		}
		gameObject.GetComponent<SpriteRenderer>().sprite = cowTex;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.level >= 5.0f){
			cowTex = cowTex1;
		}
		if (GameControl.control.exp >= 500){
			cowTex = cowTex2;
		}
		if (GameControl.control.exp >= 1000){
			cowTex = cowTex3;
		}
		gameObject.GetComponent<SpriteRenderer>().sprite = cowTex;
	}
}