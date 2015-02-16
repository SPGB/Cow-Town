using UnityEngine;
using System.Collections;

public class CowHayCollide : MonoBehaviour {

	public int hayValue = 1;
	public GameObject popup;

	void OnCollisionEnter(Collision col){
		//Debug.Log("Cow detected, destroying...");
		if (transform.position.z == 4.5f){
			GameControl.control.exp += hayValue;
			if (hayValue == 1){
				GameControl.control.totalHay++;
			} else if (hayValue == 5){
				GameControl.control.totalSpecial++;
			}
			float x = (GameObject.FindGameObjectWithTag("Cow").gameObject.transform.position.x) - 0.25f;
			float y = (GameObject.FindGameObjectWithTag("Cow").gameObject.transform.position.y) + 0.25f;
			Instantiate(popup, new Vector3(x, y, 4.5f), Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	public int getHayValue(){
		return hayValue;
	}
}
