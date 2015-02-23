using UnityEngine;
using System.Collections;

public class SpawnHay : MonoBehaviour {

	public GameObject prefab_hay;
	public GameObject prefab_rock;
	public Vector3 chanceRange;
	
	public float chanceX;
	public float chanceY;
	public float chance;
	public float happen;
	
	private int regulator;
	public int regulatorCap = 1;
	
	void Start () {
		//screenSizeX = new Vector3(-3.5f, 3.5f, 0.0f);
		//screenSizeY = new Vector3(4.5f, 0.0f, 0.0f);
		Debug.Log("Screen to World: X: " + GameControl.control.screenSizeX1.x + " / " + GameControl.control.screenSizeX2.x + " | Y: " + GameControl.control.screenSizeY.y);
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0.0f){
			if (regulator >= regulatorCap){
				chanceX = (chanceRange.x);
				chanceY = (chanceRange.y);
				chance = Random.Range(chanceX, chanceY);
				happen = (chanceRange.z);
				if (chance >= (happen - (0.5f + (0.02f * (GameControl.control.level - 1)))) && chance <= (happen + (0.5f + (0.02 * (GameControl.control.level - 1))))){
					bool is_rock = Random.Range(0, 5) < 2;
					GameObject new_object = (is_rock)? prefab_rock : prefab_hay;
					int value = new_object.GetComponent<HayCollide>().getHayValue();
					if ((value == 5 && GameControl.control.level < 5)){
						return;
					}
					if (!is_rock) Instantiate(new_object, new Vector3(Random.Range(GameControl.control.screenSizeX1.x, GameControl.control.screenSizeX2.x), GameControl.control.screenSizeY.y, 4.4f), Quaternion.identity);
					else Instantiate(new_object, new Vector3(Random.Range(GameControl.control.screenSizeX1.x, GameControl.control.screenSizeX2.x), GameControl.control.screenSizeY.y, 4.6f), Quaternion.identity);
				}
				regulator = 0;
			}
			regulator++;
		}
	}
}
