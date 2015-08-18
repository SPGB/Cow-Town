using UnityEngine;
using System.Collections;

public class sky : MonoBehaviour {

	public float speed = 0.0001f;
	public float seconds = 1;
	public bool is_spinning = false;
	// Update is called once per frame
	IEnumerator spin(float seconds) {
		yield return new WaitForSeconds(seconds);
		// Below was used for testing a real time day/night cycle.
		//if (!GameControl.control.pause) transform.rotation = Quaternion.Euler (0.0f, 0.0f, (float)System.DateTime.Now.TimeOfDay.TotalMinutes / 4);
		if (!GameControl.control.pause) transform.Rotate(0f, 0f, speed);
		is_spinning = false;
	}

	void Update () {
		if (!is_spinning) {
			is_spinning = true;
			StartCoroutine(spin(seconds));
		}

		/**
		 * Below was used for testing a real time day/night cycle.
		 * 
		float angle = (float)System.DateTime.Now.TimeOfDay.TotalMinutes;
		Debug.Log ("SKY ANGLE: " + (angle / 4) + " / MIN ANGLE: " + (((0 * 60) + 0) / 4) + " / MAX ANGLE: " + (((23 * 60) + 59) / 4));
		transform.rotation = Quaternion.Euler (0.0f, 0.0f, angle / 4);
		**/
	}
}
