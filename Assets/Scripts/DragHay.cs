using UnityEngine;
using System.Collections;

public class DragHay : MonoBehaviour {
	
	public GameObject obj;
	
	private Ray ray;
	private RaycastHit hit;
	private Vector3 cameraTransform;
	private Vector3 target;
	
	private bool selected;
	
	// Update is called once per frame
	void Update () {
		#if (UNITY_EDITOR || UNITY_STANDALONE)
		if (Input.GetMouseButton(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100.0f)){
				Debug.DrawRay(ray.origin, ray.direction);
				if (hit.transform.gameObject.tag == "Hay"){
					obj = hit.transform.gameObject;
					selected = true;
					GameControl.control.haySelected = obj;
					cameraTransform = Camera.main.transform.InverseTransformPoint(0, 0, 0);
					obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraTransform.z + 4.5f));
				}
			}
			if (selected){
				target.z = 4.5f;
				obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, 1.0f);
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			selected = false;
			GameControl.control.haySelected = null;
			obj.transform.Translate(0.0f, 0.0f, 0.1f);
			obj = null;
		}
		#endif
		
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
				obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, 1.0f);
			}
		}
		#endif
	}
}
