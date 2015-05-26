using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exp : MonoBehaviour { //experience bar for cow
	public Texture background_texture; //the backing
	public Texture foreground_texture; //the blue bar

	void Update () {
		GameControl.control.level = (100 * (GameControl.control.exp / (GameControl.control.exp + 1000)));
		this.GetComponent<Image>().fillAmount = GameControl.control.level - (Mathf.Floor(GameControl.control.level));
	}
}