#pragma strict

var object : GameObject;

function Update () {
	
	#if (UNITY_EDITOR || UNITY_STANDALONE)
	if (Input.GetMouseButton(0)){
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit : RaycastHit;
		if (Physics.Raycast(ray, hit, 100)){
			if (hit.transform.gameObject.tag == "Hay"){
				object = hit.transform.gameObject;
				var cameraTransform = Camera.main.transform.InverseTransformPoint(0, 0, 0);
				object.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraTransform.z + 4.5f));
				if (Input.GetMouseButtonUp(0)){
					object.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraTransform.z + 4.6f));
				}
			}
		}
	}
	#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
	for (var touch : Touch in Input.touches){
		var ray = Camera.main.ScreenPointToRay(touch.position);
		var hit : RaycastHit;
		if (Physics.Raycast(ray, hit, 100)){
			if (hit.transform.gameObject.tag == "Hay"){
				object = hit.transform.gameObject;
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved){
					var cameraTransform = Camera.main.transform.InverseTransformPoint(0, 0, 0);
					object.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cameraTransform.z + 4.5f));
				} else if (touch.phase == TouchPhase.Ended){
					object.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cameraTransform.z + 4.6f));
				}
			}
		}
	}
	#endif
}