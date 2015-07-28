using UnityEngine;
using System.Collections;

public class DragHay : MonoBehaviour {

	private float z;
	public bool dragging = false;
	
	void Start () {
		z = transform.position.z;
	}
	
	void OnMouseDrag () {
		if (!GameControl.control.pause){
			GameControl.control.draggingItem = true;
			dragging = true;
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4.6f));
			GetComponent<Rigidbody>().drag = 4.0f;
		}
	}
	
	void OnMouseUp () {
		if (!GameControl.control.pause){
			GameControl.control.draggingItem = false;
			dragging = false;
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
		}
	}
}
