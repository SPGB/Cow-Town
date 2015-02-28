using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public bool direction_right = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		if (direction_right) {
			temp.x += Random.Range(0.0001f, 0.001f);
		} else {
			temp.x -= Random.Range(0.0001f, 0.001f);
		}

		transform.position = temp;
	}

	void OnBecameInvisible () {
		direction_right = !direction_right;
		Vector3 temp = transform.position;
		temp.y = Random.Range(0f, 4f);
		transform.position = temp;
		Quaternion quat = transform.rotation;
		quat.y = (direction_right)? 180.0f : 0.0f;
		transform.rotation = quat;
	}
}
