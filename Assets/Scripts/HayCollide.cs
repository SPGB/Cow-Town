using UnityEngine;
using System.Collections;

public class HayCollide : MonoBehaviour {

	public int hayValue = 1;
	public int happiness_mod = 1;
	public GameObject popup;

	void OnCollisionStay(Collision col){
		//Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Cow"){
			if (transform.position.z == 4.6f){
			
				GameControl.control.happiness += happiness_mod;
				
				GameObject new_popup = (GameObject) Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
				new_popup.GetComponent<CowExpPopup>().val = "+" + happiness_mod;

				Destroy(gameObject);
				
				if (happiness_mod < 0) return;
				
				GameControl.control.exp += hayValue;
				if (hayValue == 5){
					GameControl.control.totalSpecial++;
				}
			}
			
		} else if (col.gameObject.tag == "Trough"){
			if (happiness_mod > 0){
				if (GameControl.control.trough.get_exp() < GameControl.control.trough.get_max_exp()){

					GameControl.control.trough.set_exp(GameControl.control.trough.get_exp() + hayValue);
					GameControl.control.totalHay++;
					if (hayValue == 5){ GameControl.control.totalSpecial++; }

					GameObject new_popup = (GameObject) Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
					new_popup.GetComponent<CowExpPopup>().val = "+" + happiness_mod;
				}
				Destroy(gameObject);
			}


		}
	}
	
	public int getHayValue(){
		return hayValue;
	}
}
