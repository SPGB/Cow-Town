using UnityEngine;
using System.Collections;

public class RemoveItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void removeItem (int i) {
		GameControl.control.inventory.RemoveAt(i);
		GameControl.control.inventory.Add("empty\n0\n0\n0\ncommon");
	}
}
