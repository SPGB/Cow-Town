using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Trough : MonoBehaviour {

	private GameObject wheel1;
	private GameObject wheel2;
	private GameObject wheel3;
	public GameObject bar;
	private Vector3 mousePos;
	
	private float amount;
	public Texture foreground_texture;
	
	private float dist_from_edge = -1.0f;

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

		bar.GetComponent<Image>().fillAmount = GameControl.control.troughExp / GameControl.control.troughMaxExp;
	}
	
	void OnMouseDrag () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		amount = transform.position.x - mousePos.x;
		GameControl.control.troughPos = mousePos.x;
	}
	
	void OnMouseUp () {
		amount = 0.0f;
	} 
}
