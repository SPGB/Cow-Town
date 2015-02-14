using UnityEngine;
using System.Collections;

public class TextureCow : MonoBehaviour {

	public Sprite tex1;
	public Sprite tex2;
	public Sprite tex3;
	
	private int level;
	private int levelExpected = 0;

	// Use this for initialization
	void Start () {
		level = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<Exp>().getLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if (level != levelExpected){
			if (level == 5){
				gameObject.GetComponent<SpriteRenderer>().sprite = tex1;
				levelExpected = level;
			} else if (level == 10){
				gameObject.GetComponent<SpriteRenderer>().sprite = tex2;
				levelExpected = level;
			} else if (level == 15){
				gameObject.GetComponent<SpriteRenderer>().sprite = tex3;
				levelExpected = level;
			}
		}
	}
}
