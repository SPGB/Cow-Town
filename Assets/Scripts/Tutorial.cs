using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	
	public Texture emptyTexture;

	private bool stage1 = false;
	private int stage1run = 0;
	private bool stage2 = false;
	private int stage2run = 0;
	private bool stage3 = false;
	private bool stage4 = false;
	private int stage4run = 0;
	private bool stage5 = false;
	private int stage5run = 0;

	private string tut1 = "Drag falling hay to your cow to feed it, or to the trough under the cow to fill it.";
	private string tut2 = "Special hay called the Hay of Life will spawn, this hay is worth more.\nDrag it to your cow or trough for an extra boost.";
	private string tut3 = "The trough will feed your cow while you are offline.";
	private string tut4 = "Make sure to drag rocks in the opposite direction of your cow and trough, otherwise the cow will become unhappy.";
	private string tut5 = "Money is used to buy most upgrades, collect it by dragging it to your cow or trough.";

	// Use this for initialization
	void Start () {
		if (GameControl.control.initialRun == true) {
			stage1 = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.initialRun == false) {
			stage1 = false;
			stage2 = false;
			stage3 = false;
			stage4 = false;
			stage5 = false;
		}

		if (stage1 == true) {
			if (stage1run == 0){
				Camera.main.GetComponent<SpawnHay>().Hay(false);
				stage1run++;
			}
		} else if (stage2 == true) {
			if (stage2run == 0){
				Camera.main.GetComponent<SpawnHay>().Hay(true);
				stage2run++;
			}
		} else if (stage4 == true) {
			if (stage4run == 0){
				Camera.main.GetComponent<SpawnHay>().Rock();
				stage4run++;
			}
		} else if (stage5 == true) {
			if (stage5run == 0){
				Camera.main.GetComponent<SpawnHay>().Coin();
				stage5run++;
			}
		}

	}

	void OnGUI(){
		float width = Screen.width;
		float height = Screen.height;

		GUI.BeginGroup(new Rect (0, 0, width, height)); // left, top, width, height
			if (stage1) {
				GUI.Label(new Rect(10, 100, 100, 100), tut1, GameControl.control.text);
				if (GUI.Button(new Rect(0, 0, width, height), emptyTexture, GUIStyle.none)){
					stage2 = true;
					stage1 = false;
				}
			}
			if (stage2) {
				GUI.Label(new Rect(10, 100, 100, 100), tut2, GameControl.control.text);
				if (GUI.Button(new Rect(0, 0, width, height), emptyTexture, GUIStyle.none)){
					stage3 = true;
					stage2 = false;
				}
			}
			if (stage3) {
				GUI.Label(new Rect(10, 100, 100, 100), tut3, GameControl.control.text);
				if (GUI.Button(new Rect(0, 0, width, height), emptyTexture, GUIStyle.none)){
					stage4 = true;
					stage3 = false;
				}
			}
			if (stage4) {
				GUI.Label(new Rect(10, 100, 100, 100), tut4, GameControl.control.text);
				if (GUI.Button(new Rect(0, 0, width, height), emptyTexture, GUIStyle.none)){
					stage5 = true;
					stage4 = false;
				}
			}
			if (stage5) {
				GUI.Label(new Rect(10, 100, 100, 100), tut5, GameControl.control.text);
				if (GUI.Button(new Rect(0, 0, width, height), emptyTexture, GUIStyle.none)){
					stage5 = false;
					RemoveTutorial();
				}
			}
		GUI.EndGroup();
	}

	public void RemoveTutorial(){
		Destroy(this);
	}
}
