#pragma strict

var object : GameObject;

function Update () {
	
	var ray : Ray;
	var hit : RaycastHit;
	var cameraTransform : Vector3;
	
	var coord : Vector3;
	
	#if (UNITY_EDITOR || UNITY_STANDALONE)
	if (Input.GetMouseButton(0)){
//		GameControl.control.dragging = true;
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, hit, 100)){
			Debug.DrawRay(ray.origin, ray.direction);
			if (hit.transform.gameObject.tag == "Hay"){
				object = hit.transform.gameObject;
				cameraTransform = Camera.main.transform.InverseTransformPoint(0, 0, 0);
				object.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraTransform.z + 4.5f));
				coord.x = Input.mousePosition.x;
				coord.y = Input.mousePosition.y;
				coord.z = cameraTransform.z;
			}
		}
	}
	if (Input.GetMouseButtonUp(0)) {
//		GameControl.control.dragging = false;
		object.transform.Translate(0.0f, 0.0f, 0.1f);
		object = null;
	}
	#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
	for (var touch : Touch in Input.touches){
		ray = Camera.main.ScreenPointToRay(touch.position);
		if (Physics.Raycast(ray, hit, 100)){
			if (hit.transform.gameObject.tag == "Hay"){
				object = hit.transform.gameObject;
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary){
//					GameControl.control.dragging = true;
					cameraTransform = Camera.main.transform.InverseTransformPoint(0, 0, 0);
					object.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cameraTransform.z + 4.5f));
				} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
//					GameControl.control.dragging = false;
					object.transform.Translate(0.0f, 0.0f, 0.1f);
					object = null;
				}
			}
		}
	}
	#endif
}