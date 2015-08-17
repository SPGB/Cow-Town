using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayGUI : MonoBehaviour {	
	public GameObject moneyText;
	public GameObject milkText;

	public GameObject moneyIcon;
	public GameObject milkIcon;

	public string StatsAndShop = "STATS AND SHOP STUFF";

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

	public string InventoryItems = "INVENTORY ITEMS";

	public GameObject inventorySlot1;
	public GameObject inventorySlot2;
	public GameObject inventorySlot3;
	public GameObject inventorySlot4;
	public GameObject inventorySlot5;
	public GameObject inventorySlot6;
	public GameObject inventorySlot7;
	public GameObject inventorySlot8;
	public GameObject inventorySlot9;
	public GameObject inventorySlot10;
	public GameObject inventorySlot11;
	public GameObject inventorySlot12;

	private bool isUpdateStat = false;
	public float statUpdateRate = 0.25f;

	void Start () {

	}

	void Update () {
		stats.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GameControl.control.statOffset, 0.0f, 0.0f));
		shop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GameControl.control.shopOffset, 0.0f, 0.0f));
	}

	void OnGUI () {
		moneyText.GetComponent<Text>().text = "" + GameControl.control.money;
		milkText.GetComponent<Text>().text = "" + GameControl.control.milk;

		if (GameControl.control.pause) {
			if (!isUpdateStat) {
				isUpdateStat = true;
				StartCoroutine(statsPanel(statUpdateRate));
			}

		}
	}
	IEnumerator statsPanel(float seconds) {
		yield return new WaitForSeconds (seconds);
		updateStats ();
		updateInventory ();
		updateShop ();
		isUpdateStat = false;
	}
	private void updateStats () {
		name.GetComponent<Text>().text = GameControl.control.cow.getName();

		happiness.GetComponent<Text>().text = GameControl.control.happiness.ToString("F1");
		experience.GetComponent<Text>().text = GameControl.control.exp.ToString("F1");
		trough.GetComponent<Text>().text = GameControl.control.troughExp.ToString("F1");

		strength.GetComponent<Text>().text = GameControl.control.newStrength.ToString("F0");
		constitution.GetComponent<Text>().text = GameControl.control.newConstitution.ToString("F0");
		intelligence.GetComponent<Text>().text = GameControl.control.newIntelligence.ToString("F0");

		skins.GetComponent<Text>().text = GameControl.control.skinCount.ToString("F0");
		
		inventory.GetComponent<Text>().text = (GameControl.control.inventory.Count - GameControl.control.cow.nullItems).ToString("F0");
	}

	private void updateInventory () {
		Sprite[] sprites = new Sprite[12];

		for (int i = 0; i < 12; i++){
			if (!sprites[i] || sprites[i].name != GameControl.control.cow.inv_names[i]) {
				sprites[i] = (Sprite)Resources.Load("items/textures/" + GameControl.control.cow.inv_names[i], typeof(Sprite));
			}
		}

		inventorySlot1.GetComponent<Image> ().sprite = sprites [0];
		if (sprites [0].name == "empty") {
			inventorySlot1.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot1.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot2.GetComponent<Image> ().sprite = sprites [1];
		if (sprites [1].name == "empty") {
			inventorySlot2.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot2.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot3.GetComponent<Image> ().sprite = sprites [2];
		if (sprites [2].name == "empty") {
			inventorySlot3.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot3.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot4.GetComponent<Image> ().sprite = sprites [3];
		if (sprites [3].name == "empty") {
			inventorySlot4.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot4.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot5.GetComponent<Image> ().sprite = sprites [4];
		if (sprites [4].name == "empty") {
			inventorySlot5.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot5.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot6.GetComponent<Image> ().sprite = sprites [5];
		if (sprites [5].name == "empty") {
			inventorySlot6.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot6.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot7.GetComponent<Image> ().sprite = sprites [6];
		if (sprites [6].name == "empty") {
			inventorySlot7.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot7.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot8.GetComponent<Image> ().sprite = sprites [7];
		if (sprites [7].name == "empty") {
			inventorySlot8.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot8.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot9.GetComponent<Image> ().sprite = sprites [8];
		if (sprites [8].name == "empty") {
			inventorySlot9.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot9.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot10.GetComponent<Image> ().sprite = sprites [9];
		if (sprites [9].name == "empty") {
			inventorySlot10.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot10.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot11.GetComponent<Image> ().sprite = sprites [10];
		if (sprites [10].name == "empty") {
			inventorySlot11.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot11.GetComponentInChildren<Button> ().enabled = true;
		}

		inventorySlot12.GetComponent<Image> ().sprite = sprites [11];
		if (sprites [11].name == "empty") {
			inventorySlot12.GetComponentInChildren<Button> ().enabled = false;
		} else {
			inventorySlot12.GetComponentInChildren<Button> ().enabled = true;
		}
	}

	private void updateShop () {

	}
}
