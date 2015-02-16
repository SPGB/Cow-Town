using UnityEngine;
using System.Collections;

public class CowExpPopup : MonoBehaviour {

	private float die;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (die >= 60){
			Destroy(gameObject);
		}
		die++;
	}
}
