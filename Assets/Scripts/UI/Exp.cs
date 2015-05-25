using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exp : MonoBehaviour { //experience bar for cow
	
	public float expBarLength = 0.0f; //the filled in part of the bar
	private float expBarMaxLength = 215f; //the maximum exp bar length

	public Texture background_texture; //the backing
	public Texture foreground_texture; //the blue bar

	public float barMulti; //the % progress into the level

	void Start () {
		barMulti = GameControl.control.level - (Mathf.Floor(GameControl.control.level));
	}

	void Update () {
		GameControl.control.level = (100 * (GameControl.control.exp / (GameControl.control.exp + 1000)));
		this.GetComponent<Image>().fillAmount = GameControl.control.level - (Mathf.Floor(GameControl.control.level));
	}
}