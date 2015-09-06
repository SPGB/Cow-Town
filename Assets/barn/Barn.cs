using UnityEngine;
using System.Collections;

public class Barn : MonoBehaviour {

	public Sprite barnTex0;
	public Sprite barnTex1;
	public Sprite barnTex2;
	
	private Sprite barnTex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		barnTex = barnTex0;
		if (GameControl.control.barnTexture == 0) barnTex = barnTex0;
		if (GameControl.control.barnTexture == 1) barnTex = barnTex1;
		if (GameControl.control.barnTexture == 2) barnTex = barnTex2;
		gameObject.GetComponent<SpriteRenderer>().sprite = barnTex;
	}
}
