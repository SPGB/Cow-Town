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
	}
}
