using UnityEngine;
using System.Collections;

public class HayFall : MonoBehaviour {

	public float fall;
	
	private float timer = 5.0f;

	// Update is called once per frame
	void Update () {
		if (!(transform.position.y <= -2.25f)){
			transform.Translate(0.0f, -fall * Time.deltaTime, 0.0f);
		} else if (transform.position.y <= -2.25f && transform.position.z != 4.5f){
			if (timer <= 0.0f){
				Destroy(gameObject);
			}
			timer -= 0.1f;
		}
	}
}
