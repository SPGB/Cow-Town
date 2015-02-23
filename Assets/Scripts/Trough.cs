using UnityEngine;
using System.Collections;

public class Trough : MonoBehaviour {

	private GameObject wheel1;
	private GameObject wheel2;
	private GameObject wheel3;
	
	private Vector3 mousePos;
	
	private float amount;
	public float exp = 0f;
	public float max_exp = 50f;
	public float bar_offset_y = 425f;
	public float bar_offset_x = -147f;
	private float bar_multi = 5.37f;
	public float bar_height = 32;
	public Texture foreground_texture;

	// Use this for initialization
	void Start () {
		wheel1 = GameObject.Find("wheel1");
		wheel2 = GameObject.Find("wheel2");
		wheel3 = GameObject.Find("wheel3");
	}
	
	// Update is called once per frame
	void Update () {
		wheel1.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
		wheel2.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
		wheel3.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
	}
	
	void OnMouseDrag () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		amount = transform.position.x - mousePos.x;
		transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
	}
	
	void OnMouseUp () {
		amount = 0.0f;
	} 

	void OnGUI () {
		if (GameControl.control.pauseMenu) return;
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.BeginGroup (new Rect ( pos.x + bar_offset_x, pos.y + bar_offset_y, max_exp * bar_multi,bar_height));
		GUI.BeginGroup (new Rect ( 0,0, exp * bar_multi,bar_height));
		// Draw the foreground image
		GUI.DrawTexture (new Rect (0,0,max_exp * bar_multi,bar_height), foreground_texture);
		// End both Groups
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	public void set_exp(float new_exp) {
		exp = new_exp;
	}
	public float get_exp() {
		return exp;
	}
	public float get_max_exp() {
		return max_exp;
	}
}
