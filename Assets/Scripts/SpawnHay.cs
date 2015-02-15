using UnityEngine;
using System.Collections;

public class SpawnHay : MonoBehaviour {

	public GameObject prefab;
	public Vector3 chanceRange;
	public Vector2 spawnRangeX;
	
	public float chanceX;
	public float chanceY;
	public float chance;
	public float happen;
	public float happenRange;
	
	private int regulator;
	public int regulatorCap = 1;

	// Update is called once per frame
	void Update () {
		if (regulator >= regulatorCap){
			//Debug.Log("Checking...");
			int level = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<Exp>().getLevel();
			chanceX = (chanceRange.x);
			chanceY = (chanceRange.y);
			chance = Random.Range(chanceX, chanceY);
			happen = (chanceRange.z);
			happenRange = ((happen + (0.5f + (0.1f * (level - 1))) - (happen - (0.5f + (0.1f * (level - 1))))));
			if (chance >= (happen - (0.5f + (0.1f * (level - 1)))) && chance <= (happen + (0.5f + (0.1f * (level - 1))))){
				int value = prefab.GetComponent<CowHayCollide>().getHayValue();
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
