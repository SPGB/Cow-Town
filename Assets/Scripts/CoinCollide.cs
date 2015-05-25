using UnityEngine;
using System.Collections;

public class CoinCollide : MonoBehaviour {
	public GameObject popup;
	
	void OnCollisionStay(Collision col){

		if (col.gameObject.tag == "Cow" || col.gameObject.tag == "Trough"){
			GameControl.control.money += 2;
			GameControl.control.cow.award("$2");
			Destroy(gameObject);
			GameControl.control.draggingItem = false;
		}
	}
}
