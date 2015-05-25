using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExperienceLevel : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = ( GameControl.control.level ).ToString("F0");
	}
}
