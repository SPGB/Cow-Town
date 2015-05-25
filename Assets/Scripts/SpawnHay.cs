using UnityEngine;
using System.Collections;

public class SpawnHay : MonoBehaviour {

	public GameObject prefab_hay;
	public GameObject prefab_rock;
	public GameObject prefab_coin;

	bool is_spawning = false;
	private float minTime = 2.0f;
	private float maxTime = 4.0f;
	void Start () {
		Debug.Log("Screen to World: X: " + GameControl.control.screenSizeX1.x + " / " + GameControl.control.screenSizeX2.x + " | Y: " + GameControl.control.screenSizeY.y);
	}

	IEnumerator spawn(float seconds) {
		Debug.Log ("Waiting for " + seconds + " seconds");
		
		yield return new WaitForSeconds(seconds);

		bool is_rock = Random.Range(0, 5) < 2;
		bool is_coin = Random.Range(0, 5) < 2;
		GameObject new_object = (is_rock)? prefab_rock : ((is_coin)? prefab_coin : prefab_hay);

		Vector3 min_x = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		Vector3 max_x = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		int pos_x = Random.Range ( (int) min_x.x, (int) max_x.x);
		Instantiate (new_object, new Vector3 (pos_x, GameControl.control.screenSizeY.y, 4.4f), Quaternion.identity);

		is_spawning = false;
	}

	// Update is called once per frame
	void Update () {
		if(!is_spawning) {
			is_spawning = true; //Yep, we're going to spawn
			StartCoroutine(spawn(Random.Range(minTime, maxTime)));
		}
	}
}
