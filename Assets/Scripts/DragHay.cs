using UnityEngine;
using System.Collections;

public class DragHay : MonoBehaviour {

	private float z;
	
	void OnMouseDrag () {
		z = transform.position.z;
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4.6f));
		rigidbody.drag = 4.0f;
	}
	
	void OnMouseUp () {
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
	}
	
	/**
	// Update is called once per frame
	void Update () {
		#if (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
		foreach (Touch touch in Input.touches){			
			ray = Camera.main.ScreenPointToRay(touch.position);
			target = Camera.main.ScreenToWorldPoint(touch.position);
			if (Physics.Raycast(ray, out hit, 100.0f)){
				if (hit.transform.gameObject.tag == "Hay"){
					obj = hit.transform.gameObject;
					cameraTransform = Camera.main.transform.InverseTransformPoint(0, 0, 0);
					if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary){
						selected = true;
						GameControl.control.haySelected = obj;
						obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cameraTransform.z + 4.5f));
					} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
						selected = false;
						GameControl.control.haySelected = null;
						obj.transform.Translate(0.0f, 0.0f, 0.1f);
						obj = null;
					}
				}
			}
			if (selected){
				target.z = 4.5f;
				obj.rigidbody.drag = 4.0f;
				obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, 1.0f);
			}
		}
		#endif
	}
	**/
}
