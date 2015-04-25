using UnityEngine;
using System.Collections;

public class ItemCollide : MonoBehaviour {
	
	public GameObject popup;
	public string itemString;
	
	void OnCollisionStay(Collision col){
		//Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Cow"){
			if (transform.position.z > 4.4f){
				if (GameControl.control.inventory.Contains("empty\n0\n0\n0\ncommon")){
					GameObject new_popup = (GameObject) Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
					new_popup.GetComponent<CowExpPopup>().val = "ITEM";
					
					GameControl.control.inventory.Remove("empty\n0\n0\n0\ncommon");
					GameControl.control.inventory.Add(itemString);
				}
				Destroy(gameObject);
				GameControl.control.draggingItem = false;
			}
		} else if (col.gameObject.tag == "Trough"){
			if (GameControl.control.trough){
				GameControl.control.money += 10;
			
				GameObject new_popup = (GameObject) Instantiate(popup, new Vector3(transform.position.x, transform.position.y, 4.5f), Quaternion.identity);
				new_popup.GetComponent<CowExpPopup>().val = "$10";
				
				Destroy(gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
