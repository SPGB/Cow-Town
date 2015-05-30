using UnityEngine;
using System.Collections;

public class HayCollide : MonoBehaviour {

	public int hayValue = 1;
	public int happiness_mod = 1;
	public GameObject popup;

	void OnCollisionStay(Collision col){
		if (GameControl.control.draggingItem && col.gameObject.tag == "Cow"){
				GameControl.control.cow.feed(happiness_mod);

				Destroy(gameObject);
				GameControl.control.draggingItem = false;
				
				if (happiness_mod < 0) return;
				
				GameControl.control.exp += hayValue;
				if (hayValue == 5){
					GameControl.control.totalSpecial++;
				}
		}
		if (col.gameObject.tag == "Trough"){
			int hay_real_value = 0;
			if ((hayValue < 0 && GameControl.control.troughExp >= 10) || GameControl.control.troughExp <= (GameControl.control.troughMaxExp - hayValue)){
				hay_real_value = hayValue;
			}
			if (happiness_mod > 0) {
				if (GameControl.control.happiness >= GameControl.control.happinessMax) GameControl.control.totalHay++;
				GameControl.control.trough.store(hay_real_value);
			} else {
				GameControl.control.trough.store( happiness_mod / 2 );
			}
			if (hayValue == 5){ GameControl.control.totalSpecial++; }
			Destroy(gameObject);
		}
	}
	
	public int getHayValue(){
		return hayValue;
	}
}
