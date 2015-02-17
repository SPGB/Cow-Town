﻿using UnityEngine;
using System.Collections;

public class HayCollide : MonoBehaviour {

	public int hayValue = 1;
	public GameObject popup;

	void OnCollisionStay(Collision col){
		if (col.gameObject.tag == "Cow"){
			if (transform.position.z == 4.5f){
				GameControl.control.exp += hayValue;
				if (hayValue == 1){
					GameControl.control.totalHay++;
				} else if (hayValue == 5){
					GameControl.control.totalSpecial++;
				}
				//float x = GameObject.FindGameObjectWithTag("Cow").gameObject.transform.position.x;
				//float y = (GameObject.FindGameObjectWithTag("Cow").gameObject.transform.position.y) + 1.2f;
				//Instantiate(popup, new Vector3(x, y, 4.5f), Quaternion.identity);
				Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
				Destroy(gameObject);
			}
		} else if (col.gameObject.tag == "Trough"){
			if (transform.position.z == 4.6f){
				if (GameControl.control.troughCurExp < GameControl.control.troughMaxExp){
					GameControl.control.troughCurExp += hayValue;
					if (hayValue == 1){
						GameControl.control.totalHay++;
					} else if (hayValue == 5){
						GameControl.control.totalSpecial++;
					}
					//float x = GameObject.FindGameObjectWithTag("Trough").gameObject.transform.position.x;
					//float y = GameObject.FindGameObjectWithTag("Trough").gameObject.transform.position.y;
					//Instantiate(popup, new Vector3(x, y, 4.5f), Quaternion.identity);
					Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
					Destroy(gameObject);
				}
			}
		}
	}
	
	public int getHayValue(){
		return hayValue;
	}
}
