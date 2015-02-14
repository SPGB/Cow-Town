using UnityEngine;
using System.Collections;

public class CowExpPopup : MonoBehaviour {

	private float die;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Dying");
		if (die >= 60){
			Debug.Log("Dead");
			Destroy(gameObject);
		}
		die++;
	}
}
