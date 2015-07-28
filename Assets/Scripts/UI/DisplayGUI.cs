using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayGUI : MonoBehaviour {	
	public GameObject moneyText;
	public GameObject milkText;

	public GameObject moneyIcon;
	public GameObject milkIcon;

	void Start () {

	}

	void OnGUI () {
		moneyText.GetComponent<Text>().text = "" + GameControl.control.money;
		milkText.GetComponent<Text>().text = "" + GameControl.control.milk;

		moneyText.GetComponent<Text> ().fontSize = (int)(26 * GameControl.control.screenMulti);
		milkText.GetComponent<Text> ().fontSize = (int)(26 * GameControl.control.screenMulti);

		moneyIcon.GetComponent<Image> ().transform.localScale = new Vector3(0.5f * GameControl.control.screenMulti, 0.5f * GameControl.control.screenMulti, 1.0f);
		milkIcon.GetComponent<Image> ().transform.localScale = new Vector3(0.25f * GameControl.control.screenMulti, 0.25f * GameControl.control.screenMulti, 1.0f);
	}
}
