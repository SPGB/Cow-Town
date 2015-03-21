using UnityEngine;
using System.Collections;

public class DragHay : MonoBehaviour {

	private float z;
	
	void Start () {
		z = transform.position.z;
	}
	
	void OnMouseDrag () {
		if (!GameControl.control.pause){
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4.6f));
			GetComponent<Rigidbody>().drag = 4.0f;
		}
	}
	
	void OnMouseUp () {
		if (!GameControl.control.pause){
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
		}
	}
}
