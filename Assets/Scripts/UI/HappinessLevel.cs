using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HappinessLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = String(GameControl.control.level);
	}
}
