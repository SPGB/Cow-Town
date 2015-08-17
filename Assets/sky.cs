using UnityEngine;
using System.Collections;

public class sky : MonoBehaviour {

	public float speed = 0.0001f;
	public float seconds = 1;
	public bool is_spinning = false;
	// Update is called once per frame
	IEnumerator spin(float seconds) {
		yield return new WaitForSeconds(seconds);
		if (!GameControl.control.pause) transform.Rotate(0f, 0f, speed);
		is_spinning = false;
	}

	void Update () {
		if (!is_spinning) {
			is_spinning = true;
			StartCoroutine(spin(seconds));
		}
	}
}
