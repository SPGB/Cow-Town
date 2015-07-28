using UnityEngine;
using System.Collections;

public class SpawnHay : MonoBehaviour {

	public GameObject prefab_hay;
	public GameObject prefab_hay2;
	public GameObject prefab_rock;
	public GameObject prefab_coin;

	public bool hayOfLife = false;

	bool is_spawning = false;
	private float maxTime = 3.0f;
	void Start () {
		Debug.Log("Screen to World: X: " + GameControl.control.screenSizeX1.x + " / " + GameControl.control.screenSizeX2.x + " | Y: " + GameControl.control.screenSizeY.y);
	}

	IEnumerator spawn(float seconds) {
		Debug.Log ("Waiting for " + seconds + " seconds");
		
		yield return new WaitForSeconds(seconds);

		bool is_rock = Random.Range(0, 10) < 2;
		bool is_coin = Random.Range(0, 5) < 2;
		bool is_haylife = Random.Range(0, 100) < 10;
		GameObject new_object = (is_rock)? prefab_rock : ((is_coin)? prefab_coin : ((is_haylife)? prefab_hay2 : prefab_hay));

		Vector3 min_x = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		Vector3 max_x = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		int pos_x = Random.Range ( (int) min_x.x, (int) max_x.x);
		float pos_z = (is_rock)? 4.7f : 4.4f;
		GameObject temp = Instantiate (new_object, new Vector3 (pos_x, GameControl.control.screenSizeY.y, pos_z), Quaternion.identity) as GameObject;
		Destroy (temp.gameObject, 20);
		is_spawning = false;
	}

	// Update is called once per frame
	void Update () {
		if(!is_spawning && !GameControl.control.initialRun) {
			is_spawning = true; //Yep, we're going to spawn
			//StartCoroutine(spawn(Random.Range(GameControl.control.minSpawnTime, GameControl.control.maxSpawnTime)));
			StartCoroutine(spawn(Random.Range(maxTime - 2.0f, maxTime)));
		}

		if (hayOfLife) {
			Hay (0, 0, true);
			hayOfLife = false;
		}
	}

	public void Hay(float x, float y, bool is_haylife){
		GameObject new_object = (is_haylife)? prefab_hay2 : prefab_hay;
		
		Vector3 min_x = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		Vector3 max_x = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		int pos_x = (x == 0)? Random.Range ( (int) min_x.x, (int) max_x.x) : (int) x;
		int pos_y = (y == 0) ? (int) GameControl.control.screenSizeY.y : (int) y;
		GameObject temp = Instantiate (new_object, new Vector3 (pos_x, pos_y, 4.4f), Quaternion.identity) as GameObject;
		Destroy (temp.gameObject, 20);
	}
	public void Rock(float x, float y){
		GameObject new_object = prefab_rock;
		
		Vector3 min_x = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		Vector3 max_x = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		int pos_x = (x == 0)? Random.Range ( (int) min_x.x, (int) max_x.x) : (int) x;
		int pos_y = (y == 0) ? (int) GameControl.control.screenSizeY.y : (int) y;
		GameObject temp = Instantiate (new_object, new Vector3 (pos_x, pos_y, 4.7f), Quaternion.identity) as GameObject;
		Destroy (temp.gameObject, 20);
	}
	public void Coin(float x, float y){
		GameObject new_object = prefab_coin;
		
		Vector3 min_x = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		Vector3 max_x = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		int pos_x = (x == 0)? Random.Range ( (int) min_x.x, (int) max_x.x) : (int) x;
		int pos_y = (y == 0) ? (int) GameControl.control.screenSizeY.y : (int) y;
		GameObject temp = Instantiate (new_object, new Vector3 (pos_x, pos_y, 4.4f), Quaternion.identity) as GameObject;
		Destroy (temp.gameObject, 20);
	}

	public float getMaxTime(){
		return maxTime;
	}
	public void setMaxTime(float time){
		maxTime = time;
	}
}
