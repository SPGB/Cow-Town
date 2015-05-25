using UnityEngine;
using System.Collections;

public class SpawnHay : MonoBehaviour {

	public GameObject prefab_hay;
	public GameObject prefab_rock;
	public GameObject prefab_coin;

	bool is_spawning = false;
	private float minTime = 1.0f;
	private float maxTime = 3.0f;
	void Start () {
		Debug.Log("Screen to World: X: " + GameControl.control.screenSizeX1.x + " / " + GameControl.control.screenSizeX2.x + " | Y: " + GameControl.control.screenSizeY.y);
	}

	IEnumerator spawn(float seconds) {
		Debug.Log ("Waiting for " + seconds + " seconds");
		
		yield return new WaitForSeconds(seconds);

		bool is_rock = Random.Range(0, 5) < 2;
		bool is_coin = Random.Range(0, 5) < 2;
		GameObject new_object = (is_rock)? prefab_rock : ((is_coin)? prefab_coin : prefab_hay);

		Instantiate (new_object, new Vector3 (Random.Range (GameControl.control.screenSizeX1.x + 20, GameControl.control.screenSizeX2.x - 20), GameControl.control.screenSizeY.y, 4.4f), Quaternion.identity);


		//We've spawned, so now we could start another spawn     
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
