using UnityEngine;
using System.Collections;

public class CoinCollide : MonoBehaviour {
	public GameObject popup;
	
	void OnCollisionStay(Collision col){

		if (col.gameObject.tag == "Cow" || col.gameObject.tag == "Trough"){
				GameControl.control.money++;
				
				GameObject new_popup = (GameObject) Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
				new_popup.GetComponent<CowExpPopup>().val = "$1";
				
				Destroy(gameObject);
				GameControl.control.draggingItem = false;
		}
	}
}
