using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cow : MonoBehaviour {

	//textures
	public Texture menu;
	public Texture closeButton;
	public Texture shopButton;
	public Texture moneyBackground;
	public Texture blankShopButton;
	public Texture blankBasicButton;
	public Texture emptyTexture;
	public Texture statPopup;
	
	public Texture switch1;
	public Texture switch2;
	public Texture switch3;
	
	
	public int nullItems;
	
	//game objects
	public GameObject items;
	public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	public GameObject item4;

	//text
	public GameObject floatText;

	public DateTime born;
	public DateTime now;
	public TimeSpan age;
	
	private int selected1 = -1;
	private int selected2 = -1;
	private Vector2 selected1Pos;
	private Vector2 selected2Pos;
	
	//list
	public List<string> inv_names = new List<string>();
	public List<string> inv_int = new List<string>();
	public List<string> inv_str = new List<string>();
	public List<string> inv_con = new List<string>();
	public List<string> inv_rarity = new List<string>();
	

	void Start () {
		GameControl.control.cow = this;
		
		born = DateTime.Now;
		
		items = GameObject.Find("items");
		
		for (int i = 0; i < 12; i++){
			inv_names.Add("empty");
			inv_int.Add("0");
			inv_str.Add("0");
			inv_con.Add("0");
			inv_rarity.Add("common");
		}
		
		for (int i = 0; i < 12; i++){
			if (GameControl.control.inventory.Count != 12) GameControl.control.inventory.Add("empty\n0\n0\n0\ncommon");
			else break;
		}
		
		selected1 = -1;
		selected2 = -1;
	}
	

	void Update () {
		
		for (int i = 0; i < GameControl.control.inventory.Count; i++){ //draw inventory
			string[] split = GameControl.control.inventory[i].Split();
			inv_names[i] = split[0].ToString();
			inv_int[i] = split[1].ToString();
			inv_str[i] = split[2].ToString();
			inv_con[i] = split[3].ToString();
			inv_rarity[i] = split[4].ToString();
		}
		

		item1 = (GameObject)Resources.Load("items/prefabs/" + inv_names[0], typeof(GameObject));
		item1.transform.localPosition = new Vector3(item1.transform.localPosition.x, item1.transform.localPosition.y, -0.05f);
		item2 = (GameObject)Resources.Load("items/prefabs/" + inv_names[1], typeof(GameObject));
		item2.transform.localPosition = new Vector3(item2.transform.localPosition.x, item2.transform.localPosition.y, -0.1f);
		item3 = (GameObject)Resources.Load("items/prefabs/" + inv_names[2], typeof(GameObject));
		item3.transform.localPosition = new Vector3(item3.transform.localPosition.x, item3.transform.localPosition.y, -0.15f);
		item4 = (GameObject)Resources.Load("items/prefabs/" + inv_names[3], typeof(GameObject));
		item4.transform.localPosition = new Vector3(item4.transform.localPosition.x, item4.transform.localPosition.y, -0.2f);
		
		items.transform.GetChild(0).localPosition = item1.transform.localPosition;
		items.transform.GetChild(0).localScale = item1.transform.localScale;
		items.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item1.GetComponent<SpriteRenderer>().sprite;
		
		items.transform.GetChild(1).localPosition = item2.transform.localPosition;
		items.transform.GetChild(1).localScale = item2.transform.localScale;
		items.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = item2.GetComponent<SpriteRenderer>().sprite;
		
		items.transform.GetChild(2).localPosition = item3.transform.localPosition;
		items.transform.GetChild(2).localScale = item3.transform.localScale;
		items.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = item3.GetComponent<SpriteRenderer>().sprite;
		
		items.transform.GetChild(3).localPosition = item4.transform.localPosition;
		items.transform.GetChild(3).localScale = item4.transform.localScale;
		items.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = item4.GetComponent<SpriteRenderer>().sprite;
		
		nullItems = 0;
		foreach (string item in GameControl.control.inventory){
			if (item == "empty\n0\n0\n0\ncommon") nullItems++;
		}
		
		if (selected1 != -1 && selected2 != -1){
			switchItem(selected1, selected2);
			selected1 = -1;
			selected1Pos = new Vector2(-1, -1);
			selected2 = -1;
			selected2Pos = new Vector2(-1, -1);
		}
	}
	
	public void switchItem(int itemInt1, int itemInt2){
		string switch1 = GameControl.control.inventory[itemInt1];
		string switch2 = GameControl.control.inventory[itemInt2];
		GameControl.control.inventory[itemInt1] = switch2;
		GameControl.control.inventory[itemInt2] = switch1;
	}

	void OnMouseDrag () {
		GameObject.Find ("gameControl").transform.position = new Vector3 (0.0f, 0.0f, 1.0f);
	}

	void OnMouseUp () {
		GameObject.Find ("gameControl").transform.position = new Vector3 (0.0f, 0.0f, 6.0f);
	}
	
	
	void OnGUI () {

		float shopOffset = GameControl.control.shopOffset;
		float statOffset = GameControl.control.statOffset;

		if (GUI.skin.customStyles.Length > 0) {
			GUI.skin.customStyles[0].onActive.textColor = Color.white;
		}
		
		GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		float width = Screen.width;
		float height = Screen.height;
		
		// Draw the background image for shop
		GUI.BeginGroup(new Rect (shopOffset, 0, width, height)); // left, top, width, height
			GUI.DrawTexture(new Rect (0, 0, width, height), menu);
		GUI.EndGroup();

		// Draw the background image for stats
		GUI.BeginGroup(new Rect (statOffset, 0, width, height)); // left, top, width, height
			GUI.DrawTexture(new Rect (0, 0, width, height), menu);
		GUI.EndGroup();

		// Inventory stuff
		GUI.BeginGroup(new Rect(statOffset, 0, width, height));
			GUI.DrawTexture(new Rect(10 * GameControl.control.screenMulti, 290 * GameControl.control.screenMulti, 245 * GameControl.control.screenMulti, 225 * GameControl.control.screenMulti), blankBasicButton);
			GUI.Label(new Rect(35 * GameControl.control.screenMulti, (310 * GameControl.control.screenMulti), 100, 100), "Inventory: " + (GameControl.control.inventory.Count - nullItems), GameControl.control.cowText);
			GUI.BeginGroup(new Rect(35 * GameControl.control.screenMulti, (335 * GameControl.control.screenMulti), (200 * GameControl.control.screenMulti), (150 * GameControl.control.screenMulti)));
				int i = 0;
				for (int y = 0; y < 3; y++){
					for (int x = 0; x < 4; x++){
						GUI.BeginGroup(new Rect(0 + ((x * 50) * GameControl.control.screenMulti), 0 + ((y * 50) * GameControl.control.screenMulti), (50 * GameControl.control.screenMulti), (50 * GameControl.control.screenMulti)));
							Texture image = (Texture)Resources.Load("items/textures/" + inv_names[i], typeof(Texture));
							//Texture image = (Texture)Resources.Load(inv_names[i++], typeof(Texture));
							GUI.DrawTexture(new Rect(0, 0, 50 * GameControl.control.screenMulti, 50 * GameControl.control.screenMulti), image);
							if (GameControl.control.inventory[i] != "empty\n0\n0\n0\ncommon"){
								if (GUI.Button(new Rect(30 * GameControl.control.screenMulti, 5 * GameControl.control.screenMulti, 15 * GameControl.control.screenMulti, 15 * GameControl.control.screenMulti), closeButton, GUIStyle.none)){
									GameControl.control.inventory.RemoveAt(i);
									GameControl.control.inventory.Add("empty\n0\n0\n0\ncommon");
								}
								if (GUI.Button(new Rect(0, 0, 50 * GameControl.control.screenMulti, 50 * GameControl.control.screenMulti), emptyTexture, GUIStyle.none)){
									if (selected1 == -1){
										if (selected1Pos != new Vector2(x, y) && selected2Pos != new Vector2(x, y)){
											selected1 = i;
											selected1Pos = new Vector2(x, y);
										}
									} else if (selected1 == i){
										selected1 = -1;
										selected1Pos = new Vector2(-1, -1);
										selected2 = -1;
										selected2Pos = new Vector2(-1, -1); 
									} else if (selected2 != i){
										if (selected1Pos != new Vector2(x, y) && selected2Pos != new Vector2(x, y)){
											selected2 = i;
											selected2Pos = new Vector2(x, y);
										}
									}
								}
							}
						GUI.EndGroup();
						i++;
					}
				}
			GUI.EndGroup();
			if (selected1 != -1) GUI.DrawTexture(new Rect((30 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (330 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 60 * GameControl.control.screenMulti, 60 * GameControl.control.screenMulti), (inv_rarity[selected1] == "winter" || inv_rarity[selected1] == "rare")? switch3 : ((inv_rarity[selected1] == "uncommon")? switch2 : switch1));
		// End both Groups
		GUI.EndGroup();

		// Stats
		GUI.BeginGroup(new Rect (statOffset, 0, Screen.width, Screen.height)); // left, top, width, height
			bool hapDif = (GameControl.control.happiness < GameControl.control.happinessLose);
			//Trough trough_obj = GameControl.control.trough;
			float troughExp = GameControl.control.troughExp;
			float troughMaxExp = GameControl.control.troughMaxExp;
			
			GUI.Label(new Rect(10, 50 * GameControl.control.screenMulti, 100, 100), "Betty", GameControl.control.moneyText);

			if (GameControl.control.happiness < 0.1f){
				GUI.Label(new Rect(10, 80 * GameControl.control.screenMulti, 100, 100), "Happiness:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 80 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.cowText);
			} else if (hapDif){
				GUI.Label(new Rect(10, 80 * GameControl.control.screenMulti, 100, 100), "Happiness:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 70 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.cowText);
				GUI.Label(new Rect(10, 90 * GameControl.control.screenMulti, 100, 100), "\t\t (-" + GameControl.control.happiness.ToString("F1") + "/5s)", GameControl.control.cowText);
			} else {
				GUI.Label(new Rect(10, 80 * GameControl.control.screenMulti, 100, 100), "Happiness:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 70 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.cowText);
				GUI.Label(new Rect(10, 90 * GameControl.control.screenMulti, 100, 100), "\t\t (-" + GameControl.control.happinessLose.ToString("F1") + "/5s)", GameControl.control.cowText);
			}
			
			GUI.Label(new Rect(10, 105 * GameControl.control.screenMulti, 100, 100), "Experience:", GameControl.control.cowText);
			GUI.Label(new Rect(10, 105 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.exp.ToString(), GameControl.control.cowText);
			
			if (troughExp >= 30.0f){
				int troughHours = (int)Mathf.Floor((troughExp * 2) / 60);
				int troughMinutes = (int)((troughExp * 2) - (60 * troughHours));
				if (troughMinutes > 0){
					GUI.Label(new Rect(10, 140 * GameControl.control.screenMulti, 100, 100), "Trough:", GameControl.control.cowText);
					GUI.Label(new Rect(10, 120 * GameControl.control.screenMulti, 100, 100), "\t\t " + troughExp.ToString() + " / " + troughMaxExp.ToString(), GameControl.control.cowText);
					GUI.Label(new Rect(10, 140 * GameControl.control.screenMulti, 100, 100), "\t\t (" + troughHours.ToString("F0") + " hours and", GameControl.control.cowText);
					GUI.Label(new Rect(10, 160 * GameControl.control.screenMulti, 100, 100), "\t\t " + troughMinutes.ToString("F0") + " minutes)", GameControl.control.cowText);
				} else {
					GUI.Label(new Rect(10, 140 * GameControl.control.screenMulti, 100, 100), "Trough:", GameControl.control.cowText);
					GUI.Label(new Rect(10, 130 * GameControl.control.screenMulti, 100, 100), "\t\t " + troughExp.ToString() + " / " + troughMaxExp.ToString(), GameControl.control.cowText);
					GUI.Label(new Rect(10, 150 * GameControl.control.screenMulti, 100, 100), "\t\t (" + troughHours.ToString("F0") + " hours)", GameControl.control.cowText);
				}
			} else {
				int troughMinutes = (int)(troughExp * 2);
				GUI.Label(new Rect(10, 140 * GameControl.control.screenMulti, 100, 100), "Trough:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 130 * GameControl.control.screenMulti, 100, 100), "\t\t " + troughExp.ToString() + " / " + troughMaxExp.ToString(), GameControl.control.cowText);
				GUI.Label(new Rect(10, 150 * GameControl.control.screenMulti, 100, 100), "\t\t (" + troughMinutes.ToString("F0") + " minutes)", GameControl.control.cowText);
			}
			
			int addativeStr = (GameControl.control.level >= 5)? int.Parse(inv_str[0]) + int.Parse(inv_str[1]) + int.Parse(inv_str[2]) : 0;
			int addativeCon = (GameControl.control.level >= 5)? int.Parse(inv_con[0]) + int.Parse(inv_con[1]) + int.Parse(inv_con[2]) : 0;
			int addativeInt = (GameControl.control.level >= 5)? int.Parse(inv_int[0]) + int.Parse(inv_int[1]) + int.Parse(inv_int[2]) : 0;
			if (addativeStr == 0){
				GUI.Label(new Rect(10, 180 * GameControl.control.screenMulti, 100, 100), "Strength:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 180 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.strength.ToString(), GameControl.control.cowText);
			} else {
				GUI.Label(new Rect(10, 180 * GameControl.control.screenMulti, 100, 100), "Strength:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 180 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.newStrength.ToString() + "(" + GameControl.control.strength.ToString() + ((addativeStr < 0)? "":"+") + (int.Parse(inv_str[0]) + int.Parse(inv_str[1]) + int.Parse(inv_str[2])).ToString() + ")", GameControl.control.cowText);
			}
			if (addativeCon == 0){
				GUI.Label(new Rect(10, 210 * GameControl.control.screenMulti, 100, 100), "Constitution:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 210 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.constitution.ToString(), GameControl.control.cowText);
			} else {
				GUI.Label(new Rect(10, 210 * GameControl.control.screenMulti, 100, 100), "Constitution:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 210 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.newConstitution.ToString() + "(" + GameControl.control.constitution.ToString() + ((addativeCon < 0)? "":"+") + (int.Parse(inv_con[0]) + int.Parse(inv_con[1]) + int.Parse(inv_con[2])).ToString() + ")", GameControl.control.cowText);
			}
			if (addativeInt == 0){
				GUI.Label(new Rect(10, 240 * GameControl.control.screenMulti, 100, 100), "Intelligence:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 240 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.intelligence.ToString(), GameControl.control.cowText);
			} else {
				GUI.Label(new Rect(10, 240 * GameControl.control.screenMulti, 100, 100), "Intelligence:", GameControl.control.cowText);
				GUI.Label(new Rect(10, 240 * GameControl.control.screenMulti, 100, 100), "\t\t " + GameControl.control.newIntelligence.ToString() + "(" + GameControl.control.intelligence.ToString() + ((addativeInt < 0)? "":"+") + (int.Parse(inv_int[0]) + int.Parse(inv_int[1]) + int.Parse(inv_int[2])).ToString() + ")", GameControl.control.cowText);
			}
		
			//if (GUI.Button(new Rect(width - (50 * GameControl.control.screenMulti), 10 * GameControl.control.screenMulti, (30 * GameControl.control.screenMulti), (30 * GameControl.control.screenMulti)), closeButton, GUIStyle.none)){
			//	GameControl.control.pause = false;
			//	shop = false;
			//}
		GUI.EndGroup ();

		// Shop stuff
		GUI.BeginGroup(new Rect (shopOffset, 0, width, height));
			GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.95f);
			GUI.DrawTexture(new Rect(0, 0, width, height), menu);
//			GUI.DrawTexture(new Rect((width - 100) * GameControl.control.screenMulti, 5 * GameControl.control.screenMulti, 90, 45), moneyBackground);
//			GUI.Label(new Rect((width - 70) * GameControl.control.screenMulti, 10 * GameControl.control.screenMulti, 100, 100), "" + GameControl.control.money, GameControl.control.cowText);
			
			GUI.DrawTexture(new Rect(10 * GameControl.control.screenMulti, 55 * GameControl.control.screenMulti, (215 * GameControl.control.screenMulti), (30 * GameControl.control.screenMulti)), blankShopButton); // Trough upgrade
			GUI.Label(new Rect(15 * GameControl.control.screenMulti, 60 * GameControl.control.screenMulti, 100, 100), "Trough Max +25, $500", GameControl.control.text); // Trough upgrade
			if (GUI.Button(new Rect(10 * GameControl.control.screenMulti, 55 * GameControl.control.screenMulti, 215 * GameControl.control.screenMulti, 30 * GameControl.control.screenMulti), emptyTexture, GUIStyle.none)){ // Trough upgrade
				if (GameControl.control.money > 500){
					GameControl.control.troughMaxExp += 25.0f;
					GameControl.control.money -= 500;
				}
			}
			
		GUI.EndGroup();

		// Item popups
		GUI.BeginGroup(new Rect(statOffset, 0, width, height));
			GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			if (selected1 != -1){
				GUI.DrawTexture(new Rect((45 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (290 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti), statPopup);
				GUI.Label(new Rect((50 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (295 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti), "Str:", GameControl.control.stats);
				GUI.Label(new Rect((50 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (295 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti), "          " + inv_str[selected1], GameControl.control.stats);
				GUI.Label(new Rect((50 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (315 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti), "Con:", GameControl.control.stats);
				GUI.Label(new Rect((50 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (315 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti), "          " + inv_con[selected1], GameControl.control.stats);
				GUI.Label(new Rect((50 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (335 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti), "Int:", GameControl.control.stats);
				GUI.Label(new Rect((50 + (selected1Pos.x * 50)) * GameControl.control.screenMulti, (335 + (selected1Pos.y * 50)) * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti, 70 * GameControl.control.screenMulti), "          " + inv_int[selected1], GameControl.control.stats);
			}
		GUI.EndGroup();
	}

	public void feed(int amount) {
		GameControl.control.happiness += amount;
		string amount_prefix = (amount > 0)? "+" : "";
		GameObject temp = initFloatText(amount_prefix + amount.ToString ());
		temp.GetComponent<Animator>().SetTrigger( (amount > 0)? "hit" : "miss" );
	}
	public void award(string text) {
		GameObject temp = initFloatText(text);
		temp.GetComponent<Animator>().SetTrigger("award");
	}
	GameObject initFloatText(string text) {
		Debug.Log("New hit: " + text);
		GameObject temp = Instantiate (floatText) as GameObject;
		temp.SetActive (true);

		RectTransform tempRect = temp.GetComponent<RectTransform>();
		temp.transform.SetParent(transform.FindChild("Canvas"));
		tempRect.transform.localPosition = floatText.transform.localPosition;
		tempRect.transform.localScale = floatText.transform.localScale;

		temp.GetComponent<Text>().text = text;


		Destroy (temp.gameObject, 1);
		return temp;
	}
}