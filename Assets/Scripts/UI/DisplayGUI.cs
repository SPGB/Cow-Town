using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayGUI : MonoBehaviour {	
	public GameObject moneyText;
	public GameObject milkText;

	public GameObject moneyIcon;
	public GameObject milkIcon;

	public GameObject stats;
	public GameObject shop;

	public GameObject name;
	public GameObject happiness;
	public GameObject experience;
	public GameObject trough;
	public GameObject strength;
	public GameObject constitution;
	public GameObject intelligence;
	public GameObject skins;
	public GameObject inventory;

	void Start () {

	}

	void Update () {
		stats.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GameControl.control.statOffset, stats.transform.position.y, 0.0f));
		shop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GameControl.control.shopOffset, shop.transform.position.y, 0.0f));
	}

	void OnGUI () {
		moneyText.GetComponent<Text>().text = "" + GameControl.control.money;
		milkText.GetComponent<Text>().text = "" + GameControl.control.milk;

		updateStats ();
	}

	private void updateStats () {
		name.GetComponent<Text>().text = "" + GameControl.control.cow.getName();

		happiness.GetComponent<Text>().text = "" + GameControl.control.happiness;
		experience.GetComponent<Text>().text = "" + GameControl.control.exp;
		trough.GetComponent<Text>().text = "" + GameControl.control.troughExp;

		strength.GetComponent<Text>().text = "" + GameControl.control.newStrength;
		constitution.GetComponent<Text>().text = "" + GameControl.control.newConstitution;
		intelligence.GetComponent<Text>().text = "" + GameControl.control.newIntelligence;

		skins.GetComponent<Text>().text = "" + GameControl.control.skinCount;
		
		inventory.GetComponent<Text>().text = "" + (GameControl.control.inventory.Count - GameControl.control.cow.nullItems);
	}
}
