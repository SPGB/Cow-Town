using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public bool direction_right = true;

	private float range_min = 0.0001f;
	private float range_max = 0.005f;
	public float spawn_y_min = 0f;
	public float spawn_y_max = 80f;
	void Start () {
	
	}

	void Update () {
		if (!GameControl.control.pause){
			Vector3 temp = transform.position;
			if (direction_right) {
				temp.x += Random.Range(range_min, range_max);
			} else {
				temp.x -= Random.Range(range_min, range_max);
			}
			
			transform.position = temp;
		}
	}

	void OnBecameInvisible () {
		direction_right = !direction_right;
		Vector3 temp = transform.position;
		temp.y = Random.Range(spawn_y_min, spawn_y_max);
		transform.position = temp;
		Quaternion quat = transform.rotation;
		quat.y = (direction_right)? 0f : 180f;
		transform.rotation = quat;
	}
}
