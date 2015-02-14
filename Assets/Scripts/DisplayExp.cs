using UnityEngine;
using System.Collections;

public class DisplayExp : MonoBehaviour {

	Exp exp;
	
	// Use this for initialization
	void Start () {
		exp = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<Exp>();
	}
	
	// Update is called once per frame
	void Update () {
		this.guiText.text = "Exp: " + exp.getExpCurrent().ToString() + " / " + exp.getExpNeeded().ToString();
	}
}
