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
					GameControl.control.cow.award("+Item");

					GameControl.control.inventory.Remove("empty\n0\n0\n0\ncommon");
					GameControl.control.inventory.Add(itemString);
				}
				Destroy(gameObject);
				GameControl.control.draggingItem = false;
				transform.gameObject.GetComponent<DragHay>().dragging = false;
			}
		} else if (col.gameObject.tag == "Trough"){
			if (GameControl.control.trough){
				GameControl.control.money += 10;
				GameControl.control.cow.award("$10");
				Destroy(gameObject);
				GameControl.control.draggingItem = false;
				transform.gameObject.GetComponent<DragHay>().dragging = false;
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
