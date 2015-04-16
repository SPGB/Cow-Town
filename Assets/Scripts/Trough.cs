using UnityEngine;
using System.Collections;

public class Trough : MonoBehaviour {

	private GameObject wheel1;
	private GameObject wheel2;
	private GameObject wheel3;
	
	private Vector3 mousePos;
	
	private float amount;
//	public float exp = 0f;
//	public float max_exp = 50.0f;
	public float bar_offset_y = 425f;
	public float bar_offset_x = -147f;
	public float bar_multi = 5.37f;
	public float bar_width;
	public float bar_height = 32;
	public Texture foreground_texture;
	
	private float dist_from_edge = 1.0f;

	// Use this for initialization
	void Start () {
		GameControl.control.trough = this;
		
		GameControl.control.Load();
		
		wheel1 = GameObject.Find("wheel1");
		wheel2 = GameObject.Find("wheel2");
		wheel3 = GameObject.Find("wheel3");

		//if (!GameControl.control.trough) {
		//	GameControl.control.trough = this;
		//	GameControl.control.Load(true, false);
		//	Debug.Log("setting trough");
		//}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(GameControl.control.troughPos, transform.position.y, transform.position.z);
		if (transform.position.x < GameControl.control.screenSizeX1.x + dist_from_edge){
			Vector3 temp = new Vector3(GameControl.control.screenSizeX1.x + dist_from_edge, transform.position.y, transform.position.z);
			transform.position = temp;
			amount = 0.0f;
		} else if (transform.position.x > GameControl.control.screenSizeX2.x - dist_from_edge){
			Vector3 temp = new Vector3(GameControl.control.screenSizeX2.x - dist_from_edge, transform.position.y, transform.position.z);
			transform.position = temp;
			amount = 0.0f;
		}
		
		wheel1.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
		wheel2.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
		wheel3.transform.Rotate(0.0f, 0.0f, amount * 50, Space.Self);
		
		bar_width = (50f * bar_multi) * (GameControl.control.troughExp / GameControl.control.troughMaxExp);
	}
	
	void OnMouseDrag () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		amount = transform.position.x - mousePos.x;
		GameControl.control.troughPos = mousePos.x;
	}
	
	void OnMouseUp () {
		amount = 0.0f;
	} 

	void OnGUI () {
		if (GameControl.control.pause) return;
		Vector3 trough = gameObject.transform.position;
		Vector3 trans = Camera.main.WorldToScreenPoint(new Vector3(trough.x - 1.555f, trough.y + 6.115f, trough.z));
		GUI.BeginGroup (new Rect (trans.x, trans.y, (GameControl.control.troughMaxExp * bar_multi) * GameControl.control.screenMulti, bar_height * GameControl.control.screenMulti));
		GUI.BeginGroup (new Rect ( 0,0, bar_width * GameControl.control.screenMulti, bar_height * GameControl.control.screenMulti));
		// Draw the foreground image
		GUI.DrawTexture (new Rect (0,0, 50f * bar_multi,bar_height), foreground_texture);
		// End both Groups
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	/**
	public void set_exp(float new_exp) {
		exp = new_exp;
	}
	public float get_exp() {
		return exp;
	}
	public void set_max_exp(float new_max_exp){
		max_exp = new_max_exp;
	}
	public float get_max_exp() {
		return max_exp;
	}
	**/
}
