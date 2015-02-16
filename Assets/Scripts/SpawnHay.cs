using UnityEngine;
using System.Collections;

public class SpawnHay : MonoBehaviour {

	public GameObject prefab;
	public Vector3 chanceRange;
	
	public float chanceX;
	public float chanceY;
	public float chance;
	public float happen;
	
	private int regulator;
	public int regulatorCap = 1;
	
	public Vector3 screenSizeX;
	public Vector3 screenSizeY;
	
	void Start () {
		#if (UNITY_STANDALONE || UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
		screenSizeX = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelWidth, 0));
		screenSizeY = camera.ScreenToWorldPoint(new Vector3(Screen.height + 100, 0, 0));
		#endif
		
		//#if UNITY_EDITOR
		//screenSizeX = new Vector3(-3.5f, 3.5f, 0.0f);
		//screenSizeY = new Vector3(4.5f, 0.0f, 0.0f);
		//#endif
		Debug.Log("Screen to World: X: " + screenSizeX.x + " / " + screenSizeX.y + " | Y: " + screenSizeY.x);
	}

	// Update is called once per frame
	void Update () {
		if (regulator >= regulatorCap){
			chanceX = (chanceRange.x);
			chanceY = (chanceRange.y);
			chance = Random.Range(chanceX, chanceY);
			happen = (chanceRange.z);
			if (chance >= (happen - (0.5f + (0.1f * (GameControl.control.level - 1)))) && chance <= (happen + (0.5f + (0.1f * (GameControl.control.level - 1))))){
				int value = prefab.GetComponent<CowHayCollide>().getHayValue();
				if ((value == 5 && GameControl.control.level < 5)){
					return;
				}
				Instantiate(prefab, new Vector3(Random.Range(screenSizeX.x, screenSizeX.y), screenSizeY.x, 4.6f), Quaternion.identity);
			}
			regulator = 0;
		}
		regulator++;
	}
}
