using UnityEngine;
using System.Collections;

public class SpawnHay : MonoBehaviour {

	public GameObject prefab;
	public Vector3 chanceRange;
	public Vector2 spawnRangeX;
	
	private int regulator;
	public int regulatorCap = 1;

	// Update is called once per frame
	void Update () {
		if (regulator >= regulatorCap){
			//Debug.Log("Checking...");
			int chance = (int)Random.Range(chanceRange.x, chanceRange.y);
			int happen = (int)chanceRange.z;
			if (chance == happen){
				int value = prefab.GetComponent<CowHayCollide>().getHayValue();
				int level = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<Exp>().getLevel();
				if ((value == 5 && level < 10)){
					Debug.Log("Spawn Cancelled... " + prefab.name.ToString());
					return;
				}
				Debug.Log("Spawning... " + prefab.name.ToString());
				Instantiate(prefab, new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), 3.0f, 4.6f), Quaternion.identity);
			}
			regulator = 0;
		}
		regulator++;
	}
}
