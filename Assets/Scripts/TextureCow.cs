using UnityEngine;
using System.Collections;

public class TextureCow : MonoBehaviour {

	public Sprite cowTex0;
	public Sprite cowTex1;
	public Sprite cowTex2;
	public Sprite cowTex3;
	public Sprite cowTex4;
	public Sprite cowTex5;
	public Sprite cowTex6;
	
	private Sprite cowTex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		cowTex = cowTex0;
		if (GameControl.control.cowTexture == 0) cowTex = cowTex0;
		if (GameControl.control.cowTexture == 1) cowTex = cowTex1;
		if (GameControl.control.cowTexture == 2) cowTex = cowTex2;
		if (GameControl.control.cowTexture == 3) cowTex = cowTex3;
		if (GameControl.control.cowTexture == 4) cowTex = cowTex4;
		if (GameControl.control.cowTexture == 5) cowTex = cowTex5;
		if (GameControl.control.cowTexture == 6) cowTex = cowTex6;
		gameObject.GetComponent<SpriteRenderer>().sprite = cowTex;
	}

	public void setSkin (int tex) {
		GameControl.control.cowTexture = tex;
	}
}