using UnityEngine;
using System;
using System.Collections;

public class DressLace : MonoBehaviour {

	public Sprite lace1;
	public Sprite lace2;

	// Use this for initialization
	void Start () {
		DateTime lace = DateTime.Now;
		if (lace.Second % 2 == 0) gameObject.GetComponent<SpriteRenderer>().sprite = lace2;
		else gameObject.GetComponent<SpriteRenderer>().sprite = lace1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
