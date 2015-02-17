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
	
	public Vector3 screenSizeX1;
	public Vector3 screenSizeX2;
	public Vector3 screenSizeY;
	
	void Start () {
		#if (UNITY_STANDALONE || UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
		screenSizeX1 = camera.ScreenToWorldPoint(new Vector3(50, 0, 0));
		screenSizeX2 = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth - 50, 0, 0));
		screenSizeY = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight + 100, 0));
		#endif
		
		//#if UNITY_EDITOR
		//screenSizeX = new Vector3(-3.5f, 3.5f, 0.0f);
		//screenSizeY = new Vector3(4.5f, 0.0f, 0.0f);
		//#endif
		Debug.Log("Screen to World: X: " + screenSizeX1.x + " / " + screenSizeX2.x + " | Y: " + screenSizeY.y);
	}

	// Update is called once per frame
	void Update () {
		if (regulator >= regulatorCap){
			chanceX = (chanceRange.x);
			chanceY = (chanceRange.y);
			chance = Random.Range(chanceX, chanceY);
			happen = (chanceRange.z);
			if (chance >= (happen - (0.5f + (0.1f * (GameControl.control.level - 1)))) && chance <= (happen + (0.5f + (0.1f * (GameControl.control.level - 1))))){
				int value = prefab.GetComponent<HayCollide>().getHayValue();
				if ((value == 5 && GameControl.control.level < 5)){
					return;
				}
				Instantiate(prefab, new Vector3(Random.Range(screenSizeX1.x, screenSizeX2.x), screenSizeY.y, 4.6f), Quaternion.identity);
			}
			regulator = 0;
		}
		regulator++;
	}
}
