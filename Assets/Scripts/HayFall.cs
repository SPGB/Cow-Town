using UnityEngine;
using System;
using System.Collections;

public class HayFall : MonoBehaviour {

	private bool startTime;
	
	private DateTime deleteTime1;
	private DateTime deleteTime2;
	private TimeSpan deleteTimeSpan;

	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -3.25f){
			if (!startTime){
				deleteTime1 = DateTime.Now;
				startTime = true;
			}
			deleteTime2 = DateTime.Now;
			deleteTimeSpan = deleteTime2 - deleteTime1;
			if (deleteTimeSpan.TotalSeconds > 2.5){
				Destroy(gameObject);
			}
		}
		if (rigidbody.drag > 0.05f)	rigidbody.drag = rigidbody.drag - 0.05f;
	}
}
