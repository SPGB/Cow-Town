using UnityEngine;
using System.Collections;

public class HayCollide : MonoBehaviour {

	public int hayValue = 1;
	public int happiness_mod = 1;
	public GameObject popup;

	void OnCollisionStay(Collision col){
		//Debug.Log(col.gameObject.tag);
		if (GameControl.control.draggingItem && col.gameObject.tag == "Cow"){
				GameControl.control.happiness += happiness_mod;
				
				GameObject new_popup = (GameObject) Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
				new_popup.GetComponent<CowExpPopup>().val = ( (happiness_mod > 0)? "+" : "") + happiness_mod.ToString();

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
			GameControl.control.happiness += happiness_mod;

			if (happiness_mod > 0 && GameControl.control.happiness >= GameControl.control.happinessMax) {
				GameControl.control.totalHay++;
			}
			if (hayValue == 5){ GameControl.control.totalSpecial++; }

			GameObject new_popup = (GameObject) Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
			new_popup.GetComponent<CowExpPopup>().val = ( (happiness_mod > 0)? "+" : "") + happiness_mod.ToString();
			Destroy(gameObject);
		}
	}
	
	public int getHayValue(){
		return hayValue;
	}
}
