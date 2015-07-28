using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	
//	public Texture emptyTexture;
//
//	private bool stage1 = false;
//	private int stage1run = 0;
//	private bool stage2 = false;
//	private int stage2run = 0;
//	private bool stage3 = false;
//	private bool stage4 = false;
//	private int stage4run = 0;
//	private bool stage5 = false;
//	private int stage5run = 0;

//	private string tut1 = "Drag falling hay to your cow to feed it,\nor to the trough under the cow to fill it.";
//	private string tut2 = "Special hay called the Hay of Life will\nspawn, this hay is worth more. Drag it to\nyour cow or trough for an extra boost.";
//	private string tut3 = "The trough will feed your cow while you\nare offline.";
//	private string tut4 = "Make sure to drag rocks in the opposite\ndirection of your cow and trough, otherwise\nthe cow will become unhappy.";
//	private string tut5 = "Money is used to buy most upgrades, collect\nit by dragging it to your cow or trough.";

	private string[] tutorials = new string[8];
	private int[] timers = new int[8];

	private int stage = 0;
	private bool showGUI = false;
	private bool showTAP = false;

	// Use this for initialization
	void Start () {
		if (GameControl.control.initialRun == true) {
//			stage1 = true;

			tutorials[0] = "Drag hay to your cow to feed it,\nor to the trough under the cow to fill it.";
			tutorials[1] = "Special hay called the Hay of Life can\nappear, this hay is worth more. Drag it to\nyour cow or trough for an extra boost.";
			tutorials[2] = "The trough under your cow will feed\nyour cow while you are offline.";
			tutorials[3] = "Make sure to drag rocks in the opposite\ndirection of your cow and trough\notherwise the cow will become unhappy.";
			tutorials[4] = "Money is used to buy most upgrades,\ncollect it by dragging it to your cow or\ntrough.";
			tutorials[5] = "Items will occasionally drop from\nthe top, drag them to your cow to earn\nbonuses.";
			tutorials[6] = "Drag from the right side of the screen\nfor your cow's stats and inventory,\nthen drag right to hide it again.";
			tutorials[7] = "Drag from the left side of the screen\nfor the store, you can buy a bunch\nof upgrades in there, try it now.";

			timers[0] = 2;
			timers[1] = 4;
			timers[2] = 4;
			timers[3] = 2;
			timers[4] = 4;
			timers[5] = 4;
			timers[6] = 2;
			timers[7] = 2;

			StartCoroutine(stageIEnum());


		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.initialRun == false) {
			StopAllCoroutines();
			Destroy(this);
//			stage1 = false;
//			stage2 = false;
//			stage3 = false;
//			stage4 = false;
//			stage5 = false;
		}
//
//		if (stage2 == true) {
//			if (stage2run == 0){
//				Camera.main.GetComponent<SpawnHay>().Hay(true);
//				stage2run++;
//			}
//		} else if (stage4 == true) {
//			if (stage4run == 0){
//				stage4run++;
//			}
//		} else if (stage5 == true) {
//			if (stage5run == 0){
//				Camera.main.GetComponent<SpawnHay>().Coin();
//				stage5run++;
//			}
//		}
//
	}
//
	void OnGUI(){
		float width = Screen.width;
		float height = Screen.height;

		GUI.BeginGroup(new Rect (0, 0, width, height)); // left, top, width, height

		if (showGUI) GUI.Label (new Rect (10 * GameControl.control.screenMulti, 100 * GameControl.control.screenMulti, 100, 100), tutorials [stage], GameControl.control.text);
		if (showTAP) GUI.Label (new Rect (10 * GameControl.control.screenMulti, 260 * GameControl.control.screenMulti, 100, 100), "Tap the screen to continue.", GameControl.control.text);
		GUI.EndGroup();
	}

	IEnumerator stageIEnum() {
		Vector2 spawnPos = new Vector2 (-1.46f, 1.46f);

		if (stage == 0) yield return new WaitForSeconds (1f);

		Debug.Log ("T" + (stage + 1) + ": Waiting for 5 seconds.");

		if (stage == 0) Camera.main.GetComponent<SpawnHay> ().Hay (spawnPos.x, spawnPos.y, false);
		else if (stage == 1) Camera.main.GetComponent<SpawnHay> ().Hay (spawnPos.x, spawnPos.y, true);
		else if (stage == 3) Camera.main.GetComponent<SpawnHay> ().Rock (spawnPos.x, spawnPos.y);
		else if (stage == 4) Camera.main.GetComponent<SpawnHay> ().Coin (spawnPos.x, spawnPos.y);
		else if (stage == 5) Camera.main.GetComponent<SpawnItems> ().Common (spawnPos.x, spawnPos.y);
		showGUI = true;
		showTAP = true;
		//GUI.Label(new Rect(10, 100, 100, 100), tutorials[stage], GameControl.control.text);

		yield return new WaitForSeconds (1);

		GameControl.control.pause = true;

		yield return new WaitForSeconds (1);

		showTAP = false;

		yield return new WaitForSeconds (7);

		Debug.Log ("T" + (stage + 1) + ": Waiting for " + timers [stage] + " seconds.");
		showGUI = false;

		yield return new WaitForSeconds (timers [stage]);

//		stage1 = false;
//		stage2 = true;

		stage++;

		if (stage < 8) {
			StartCoroutine (stageIEnum ());
		} else {
			GameControl.control.initialRun = false;
			Debug.Log ("T8: Ending Tutorial... Setting initialRun to FALSE.");
		}
	}

//	IEnumerator stage2IEnum(float seconds1, float seconds2) {
//		Debug.Log ("T2: Waiting for " + seconds1 +" seconds.");
//		
//		Camera.main.GetComponent<SpawnHay>().Hay(true);
//		GUI.Label(new Rect(10, 100, 100, 100), tut2, GameControl.control.text);
//		
//		GameControl.control.pause = true;
//		
//		yield return new WaitForSeconds(seconds1);
//		Debug.Log ("T2: Waiting for " + seconds2 + " seconds.");
//		
//		GameControl.control.pause = false;
//		
//		yield return new WaitForSeconds(seconds2);
//		
//		stage2 = false;
//		stage3 = true;
//		StartCoroutine(stage3IEnum(5f, 20f));
//	}
//
//	IEnumerator stage3IEnum(float seconds1, float seconds2) {
//		Debug.Log ("T3: Waiting for " + seconds1 + " seconds.");
//
//		GUI.Label(new Rect(10, 100, 100, 100), tut3, GameControl.control.text);
//		
//		GameControl.control.pause = true;
//		
//		yield return new WaitForSeconds(seconds1);
//		Debug.Log ("T3: Waiting for seconds2 seconds.");
//		
//		GameControl.control.pause = false;
//		
//		yield return new WaitForSeconds(seconds2);
//		
//		stage3 = false;
//		stage4 = true;
//		StartCoroutine(stage4IEnum(5f, 20f));
//	}
//
//	IEnumerator stage4IEnum(float seconds1, float seconds2) {
//		Debug.Log ("T4: Waiting for " + seconds1 + " seconds.");
//		
//		Camera.main.GetComponent<SpawnHay>().Rock();
//		GUI.Label(new Rect(10, 100, 100, 100), tut4, GameControl.control.text);
//		
//		GameControl.control.pause = true;
//		
//		yield return new WaitForSeconds(seconds1);
//		Debug.Log ("T4: Waiting for " + seconds2 + " seconds.");
//		
//		GameControl.control.pause = false;
//		
//		yield return new WaitForSeconds(seconds2);
//		
//		stage4 = false;
//		stage5 = true;
//		StartCoroutine(stage5IEnum(5f, 20f));
//	}
//
//	IEnumerator stage5IEnum(float seconds1, float seconds2) {
//		Debug.Log ("T5: Waiting for " + seconds1 + " seconds.");
//		
//		Camera.main.GetComponent<SpawnHay>().Coin();
//		GUI.Label(new Rect(10, 100, 100, 100), tut5, GameControl.control.text);
//		
//		GameControl.control.pause = true;
//		
//		yield return new WaitForSeconds(seconds1);
//		Debug.Log ("T5: Waiting for " + seconds2 + " seconds.");
//		
//		GameControl.control.pause = false;
//		
//		yield return new WaitForSeconds(seconds2);
//		
//		stage5 = false;
//
//		GameControl.control.initialRun = false;
//	}

	public void RemoveTutorial(){
		Destroy(this);
	}
}
