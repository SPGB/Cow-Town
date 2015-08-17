using UnityEngine;
using System.Collections;

public class sky : MonoBehaviour {

	public float speed = 0.0001f;

	// Update is called once per frame
	void Update () {
		if (!GameControl.control.pause) transform.Rotate(0f, 0f, speed);
	}
}
