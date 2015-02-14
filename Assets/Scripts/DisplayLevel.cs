using UnityEngine;
using System.Collections;

public class DisplayLevel : MonoBehaviour {

	Exp exp;

	// Use this for initialization
	void Start () {
		exp = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<Exp>();
	}
	
	// Update is called once per frame
	void Update () {
		this.guiText.text = "Level: " + exp.getLevel().ToString();
	}
}
