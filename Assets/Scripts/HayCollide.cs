﻿using UnityEngine;
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
			if (hayValue < 0 || GameControl.control.troughExp <= (GameControl.control.troughMaxExp - hayValue)){
				hay_real_value = hayValue;
			}

			GameControl.control.troughExp += hay_real_value;
			GameControl.control.cow.feed(happiness_mod);

			if (happiness_mod > 0 && GameControl.control.happiness >= GameControl.control.happinessMax) {
				GameControl.control.totalHay++;
			}
			if (hayValue == 5){ GameControl.control.totalSpecial++; }
			Destroy(gameObject);
		}
	}
	
	public int getHayValue(){
		return hayValue;
	}
}
