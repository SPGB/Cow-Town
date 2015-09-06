using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayGUI : MonoBehaviour {	
	public GameObject moneyText;
	public GameObject milkText;

	public GameObject moneyIcon;
	public GameObject milkIcon;

	public string StatsAndShop = "STATS AND SHOP STUFF";

	public string Stats = "STATS";

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

	public string Shop = "SHOP";

	public GameObject[] shopButton = new GameObject[4];
	public GameObject[] shopQuantity = new GameObject[4];
	public GameObject[] shopPrice = new GameObject[4];
	public GameObject[] shopOwned = new GameObject[4];

	public string InventoryItems = "INVENTORY ITEMS";

	public GameObject[] inventorySlots = new GameObject[12];

	private bool isUpdateStat = false;
	public float statUpdateRate = 0.1f;

	void Start () {

	}

	void Update () {
		stats.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GameControl.control.statOffset, 0.0f, 0.0f));
		shop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GameControl.control.shopOffset, 0.0f, 0.0f));

		if (GameControl.control.pause) {
			if (!isUpdateStat) {
				isUpdateStat = true;
				StartCoroutine(statsPanel(statUpdateRate));
			}
		}
	}

	void OnGUI () {
		moneyText.GetComponent<Text>().text = "" + GameControl.control.money;
		milkText.GetComponent<Text>().text = "" + GameControl.control.milk;
	}
	IEnumerator statsPanel(float seconds) {
		yield return StartCoroutine(WaitForRealTime(1));
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

				if (sprites [i].name == "empty") {
					inventorySlots[i].SetActive(false);
				} else {
					inventorySlots[i].SetActive(true);
				}
			}
			inventorySlots[i].GetComponent<Image>().sprite = sprites[i];
		}
	}

	private void updateShop () {
		for (int i = 0; i < 4; i++) {
			if (GameControl.control.upgradePrice [i] > GameControl.control.money) shopButton [i].GetComponent<Button>().interactable = false; 
			else if (GameControl.control.upgradePrice [i] <= GameControl.control.money) shopButton [i].GetComponent<Button>().interactable = true;

			shopPrice [i].GetComponent<Text>().text = "$" + GameControl.control.upgradePrice [i].ToString("F0");
			shopOwned [i].GetComponent<Text>().text = GameControl.control.upgradeOwned [i].ToString("F0");
		}
		shopQuantity [0].GetComponent<Text>().text = "+" + GameControl.control.upgradeQuantity [0].ToString("F0");
		shopQuantity [1].GetComponent<Text>().text = "+" + GameControl.control.upgradeQuantity [1].ToString("F0");
		shopQuantity [2].GetComponent<Text>().text = "+" + GameControl.control.upgradeQuantity [2].ToString("F1");
		shopQuantity [3].GetComponent<Text>().text = "-" + GameControl.control.upgradeQuantity [3].ToString("F1");
	}

	public static IEnumerator WaitForRealTime(float delay){
		while(true){
			float pauseEndTime = Time.realtimeSinceStartup + delay;
			while (Time.realtimeSinceStartup < pauseEndTime){
				yield return 0;
			}
			break;
		}
	}
}