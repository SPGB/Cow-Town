using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cow : MonoBehaviour {

	public Texture menu;
	public Texture closeButton;
	public Texture shopButton;
	public Texture buyButton;
	public Texture deleteSaveButton;
	private bool shop;
	
	public int nullItems;
	
	public GameObject items;
	
	public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	public GameObject item4;
	
	public DateTime born;
	public DateTime now;
	public TimeSpan age;
	
	public float strength = 10.0f;
	private float expectedStrength = 9.0f;
	public float newStrength = 10.0f;
	public float constitution = 10.0f;
	private float expectedConstitution = 9.0f;
	public float newConstitution = 10.0f;
	public float intelligence = 10.0f;
	private float expectedIntelligence = 9.0f;
	public float newIntelligence = 10.0f;
	
	private int statMin = 10;
	private int statMax = 18;
	
	/*
	public string[] inventory = new string[12];
	public string[] inv_names = new string[12];
	public string[] inv_int = new string[12];
	public string[] inv_str = new string[12];
	public string[] inv_con = new string[12];
	public string[] inv_rarity = new string[12];
	**/
	
	public List<string> inventory = new List<string>();
	public List<string> inv_names = new List<string>();
	public List<string> inv_int = new List<string>();
	public List<string> inv_str = new List<string>();
	public List<string> inv_con = new List<string>();
	public List<string> inv_rarity = new List<string>();
	
	// Use this for initialization
	void Start () {
		GameControl.control.cow = this;
		
		items = GameObject.Find("items");
		
		for (int i = 0; i < 12; i++){
			inv_names.Add("empty");
			inv_int.Add("0");
			inv_str.Add("0");
			inv_con.Add("0");
			inv_rarity.Add("common");
		}
		
		statMin = 10 + GameControl.control.numberOfCowsBred;
		statMax = 18 + GameControl.control.numberOfCowsBred;
		if (!GameControl.control.statsRandomized){
			randomizeStats(statMin, statMax, statMin, statMax, statMin, statMax);
			GameControl.control.statsRandomized = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (born != GameControl.control.cowBorn) born = GameControl.control.cowBorn;
		now = DateTime.Now;
		age = now - born;
		GameControl.control.cowAge = age;
		GameControl.control.inventory = inventory;
		
		for (int c = 0; c < 12; c++){
			if (inventory[c] == "empty\n0\n0\n0\ncommon"){
				inventory.RemoveAt(c);
				inventory.Add("empty\n0\n0\n0\ncommon");
			}
		}
		
		for (int i = 0; i < inventory.Count; i++){
			string[] split = inventory[i].Split();
			inv_names[i] = split[0].ToString();
			inv_int[i] = split[1].ToString();
			inv_str[i] = split[2].ToString();
			inv_con[i] = split[3].ToString();
			inv_rarity[i] = split[4].ToString();
		}
		
		item1 = (GameObject)Resources.Load("items/prefabs/" + inv_names[0], typeof(GameObject));
		item1.transform.localPosition = new Vector3(item1.transform.localPosition.x, item1.transform.localPosition.y, -0.1f);
		item2 = (GameObject)Resources.Load("items/prefabs/" + inv_names[1], typeof(GameObject));
		item2.transform.localPosition = new Vector3(item2.transform.localPosition.x, item2.transform.localPosition.y, -0.2f);
		item3 = (GameObject)Resources.Load("items/prefabs/" + inv_names[2], typeof(GameObject));
		item3.transform.localPosition = new Vector3(item3.transform.localPosition.x, item3.transform.localPosition.y, -0.3f);
		item4 = (GameObject)Resources.Load("items/prefabs/" + inv_names[3], typeof(GameObject));
		item4.transform.localPosition = new Vector3(item4.transform.localPosition.x, item4.transform.localPosition.y, -0.4f);
		
		if (GameControl.control.level >= 5){
			if (expectedStrength != newStrength){
				float addition = int.Parse(inv_str[0]) + int.Parse(inv_str[1]) + int.Parse(inv_str[2]);
				newStrength = strength + addition;
				expectedStrength = newStrength;
			}
			if (expectedConstitution != newConstitution){
				float addition = int.Parse(inv_con[0]) + int.Parse(inv_con[1]) + int.Parse(inv_con[2]);
				newConstitution = constitution + addition;
				expectedConstitution = newConstitution;
			}
			if (expectedIntelligence != newIntelligence){
				float addition = int.Parse(inv_int[0]) + int.Parse(inv_int[1]) + int.Parse(inv_int[2]);
				newIntelligence = intelligence + addition;
				expectedIntelligence = newIntelligence;
			}
			items.SetActive(true);
		} else {
			items.SetActive(false);
		}
		
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
		foreach (string item in inventory){
			if (item == "empty\n0\n0\n0\ncommon") nullItems++;
		}
	}
	
	public void switchItem(int itemInt1, int itemInt2){
		string switch1 = inventory[itemInt1];
		string switch2 = inventory[itemInt2];
		inventory[itemInt1] = switch2;
		inventory[itemInt2] = switch1;
	}
	
	public void randomizeStats(int strMin, int strMax, int conMin, int conMax, int intMin, int intMax){
		strength = float.Parse(UnityEngine.Random.Range(strMin, strMax).ToString("F1"));
		newStrength = strength;
		//Debug.Log("Str: " + GameControl.control.cowStrength);
		constitution = float.Parse(UnityEngine.Random.Range(conMin, conMax).ToString("F1"));
		newConstitution = constitution;
		//Debug.Log("Con: " + GameControl.control.cowConstitution);
		intelligence = float.Parse(UnityEngine.Random.Range(intMin, intMax).ToString("F1"));
		newIntelligence = intelligence;
		//Debug.Log("Int: " + GameControl.control.cowIntelligence);
	}
	
	void OnMouseDown () {
		if (!GameControl.control.pause){
			GameControl.control.pause = true;
			GameControl.control.Save();
		}
	}
	
	void OnGUI () {
		if (GameControl.control.pause){
			GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
			float width = Screen.width - 100;
			float height = Screen.height - 100;
			// Create one Group to contain both images
			// Adjust the first 2 coordinates to place it somewhere else on-screen
			GUI.BeginGroup(new Rect (50, 50, width, height)); // left, top, width, height
				// Draw the background image
				GUI.DrawTexture(new Rect (0, 0, width, height), menu);
				GUI.Label(new Rect(60, 280, 100, 100), "Inventory:\t\t\t\t" + (inventory.Count - nullItems), GameControl.control.text);
				GUI.BeginGroup(new Rect(60, 310, 200, 150));
					int i = 0;
					for (int y = 0; y < 3; y++){
						for (int x = 0; x < 4; x++){
							GUI.BeginGroup(new Rect(0 + (x * 50), 0 + (y * 50), 50, 50));
								Texture image = (Texture)Resources.Load("items/textures/" + inv_names[i], typeof(Texture));
								//Texture image = (Texture)Resources.Load(inv_names[i++], typeof(Texture));
								GUI.DrawTexture(new Rect(0, 0, 50, 50), image);
								if (inventory[i] != "empty\n0\n0\n0\ncommon"){
									if (GUI.Button(new Rect(35, 5, 10, 10), closeButton, GUIStyle.none)){
										inventory.RemoveAt(i);
										inventory.Add("empty\n0\n0\n0\ncommon");
									}
								}
							GUI.EndGroup();
							i++;
						}
					}
				GUI.EndGroup();
			// End both Groups
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
				bool hapDif = (GameControl.control.happiness < GameControl.control.happinessLose);
				Trough trough_obj = GameControl.control.trough;
				
				if (GameControl.control.happiness < 0.1f){
					GUI.Label(new Rect(110, 110, 100, 100), "Happiness:", GameControl.control.text);
					GUI.Label(new Rect(110, 110, 100, 100), "\t\t\t\t\t\t\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
				} else if (hapDif){
					GUI.Label(new Rect(110, 110, 100, 100), "Happiness:", GameControl.control.text);
					GUI.Label(new Rect(110, 100, 100, 100), "\t\t\t\t\t\t\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
					GUI.Label(new Rect(110, 120, 100, 100), "\t\t\t\t\t\t\t\t\t(-" + GameControl.control.happiness.ToString("F1") + "/5s)", GameControl.control.text);
				} else {
					GUI.Label(new Rect(110, 110, 100, 100), "Happiness:", GameControl.control.text);
					GUI.Label(new Rect(110, 100, 100, 100), "\t\t\t\t\t\t\t\t\t" + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
					GUI.Label(new Rect(110, 120, 100, 100), "\t\t\t\t\t\t\t\t\t(-" + GameControl.control.happinessLose.ToString("F1") + "/5s)", GameControl.control.text);
				}
				
				GUI.Label(new Rect(110, 145, 100, 100), "Money:\t\t\t\t\t$" + GameControl.control.money, GameControl.control.text);
				
				GUI.Label(new Rect(110, 165, 100, 100), "Experience:\t\t\t" + GameControl.control.exp.ToString(), GameControl.control.text);
				
				if (trough_obj.get_exp() >= 30.0f){
					int troughHours = (int)Mathf.Floor((trough_obj.get_exp() * 2) / 60);
					int troughMinutes = (int)((trough_obj.get_exp() * 2) - (60 * troughHours));
					if (troughMinutes > 0){
						GUI.Label(new Rect(110, 200, 100, 100), "Trough:", GameControl.control.text);
						GUI.Label(new Rect(110, 180, 100, 100), "\t\t\t\t\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString(), GameControl.control.text);
						GUI.Label(new Rect(110, 200, 100, 100), "\t\t\t\t\t\t\t\t\t(" + troughHours.ToString("F0") + " hours and", GameControl.control.text);
						GUI.Label(new Rect(110, 220, 100, 100), "\t\t\t\t\t\t\t\t\t" + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
					} else {
						GUI.Label(new Rect(110, 200, 100, 100), "Trough:", GameControl.control.text);
						GUI.Label(new Rect(110, 190, 100, 100), "\t\t\t\t\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString(), GameControl.control.text);
						GUI.Label(new Rect(110, 210, 100, 100), "\t\t\t\t\t\t\t\t\t(" + troughHours.ToString("F0") + " hours)", GameControl.control.text);
					}
				} else {
					int troughMinutes = (int)(trough_obj.get_exp() * 2);
					GUI.Label(new Rect(110, 200, 100, 100), "Trough:", GameControl.control.text);
					GUI.Label(new Rect(110, 190, 100, 100), "\t\t\t\t\t\t\t\t\t" + trough_obj.get_exp().ToString() + " / " + trough_obj.get_max_exp().ToString(), GameControl.control.text);
					GUI.Label(new Rect(110, 210, 100, 100), "\t\t\t\t\t\t\t\t\t(" + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
				}
				
				int addativeStr = int.Parse(inv_str[0]) + int.Parse(inv_str[1]) + int.Parse(inv_str[2]);
				int addativeCon = int.Parse(inv_con[0]) + int.Parse(inv_con[1]) + int.Parse(inv_con[2]);
				int addativeInt = int.Parse(inv_int[0]) + int.Parse(inv_int[1]) + int.Parse(inv_int[2]);
				GUI.Label(new Rect(110, 240, 100, 100), "Strength:\t\t\t\t" + newStrength.ToString() + "(" + strength.ToString() + ((addativeStr < 0)? "":"+") + (int.Parse(inv_str[0]) + int.Parse(inv_str[1]) + int.Parse(inv_str[2])).ToString() + ")", GameControl.control.text);
				GUI.Label(new Rect(110, 270, 100, 100), "Constitution:\t\t\t" + newConstitution.ToString() + "(" + constitution.ToString() + ((addativeCon < 0)? "":"+") + (int.Parse(inv_con[0]) + int.Parse(inv_con[1]) + int.Parse(inv_con[2])).ToString() + ")", GameControl.control.text);
				GUI.Label(new Rect(110, 300, 100, 100), "Intelligence:\t\t\t" + newIntelligence.ToString() + "(" + intelligence.ToString() + ((addativeInt < 0)? "":"+") + (int.Parse(inv_int[0]) + int.Parse(inv_int[1]) + int.Parse(inv_int[2])).ToString() + ")", GameControl.control.text);
			GUI.EndGroup ();
			
			GUI.BeginGroup(new Rect (50, 50, width, height));
				if (shop){
					GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.95f);
					GUI.DrawTexture(new Rect (0, 0, width, height), menu);
					GUI.Label(new Rect(60, 60, 100, 100), "Trough Max +25, $500:", GameControl.control.text);
					
					if (GUI.Button(new Rect(width - 130, 55, 90, 30), buyButton, GUIStyle.none)){ // Trough upgrade
						if (GameControl.control.money > 500){
							GameControl.control.trough.set_max_exp(GameControl.control.trough.get_max_exp() + 25);
							GameControl.control.money -= 500;
						}
					}
				} else {
					GUI.Label(new Rect(width - 270, height - 45, 100, 100), "Delete Save?", GameControl.control.text);
					if (GUI.Button(new Rect(width - 130, height - 50, 90, 30), deleteSaveButton, GUIStyle.none)){
						PlayerPrefs.DeleteAll();
						GameControl.control.Reset();
						GameControl.control.Save();
						GameControl.control.Load();
					}
				}
				
				if (GUI.Button(new Rect(width - 130, 10, 90, 30), closeButton, GUIStyle.none)){
					GameControl.control.pause = false;
					shop = false;
				}
				
				if (GUI.Button(new Rect(45, 10, 90, 30), shopButton, GUIStyle.none)){
					shop = !shop;
				}
			GUI.EndGroup();
		}
	}
}