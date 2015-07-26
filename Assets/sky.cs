using UnityEngine;
using System.Collections;

public class sky : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, 0f, 0.01f);
	}
}
