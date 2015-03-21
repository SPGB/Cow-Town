﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cow : MonoBehaviour {

	public Texture menu;
	public Texture closeButton;
	public Texture shopButton;
	public Texture blankButton;
	public Texture emptyTexture;
	public Texture statPopup;
	
	public Texture switch1;
	public Texture switch2;
	public Texture switch3;
	
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
	
	private int selected1 = -1;
	private int selected2 = -1;
	private Vector2 selected1Pos;
	private Vector2 selected2Pos;
	
	/*
	public string[] inventory = new string[12];
	public string[] inv_names = new string[12];
	public string[] inv_int = new string[12];
	public string[] inv_str = new string[12];
	public string[] inv_con = new string[12];
	public string[] inv_rarity = new string[12];
	**/
	
	//public List<string> inventory = new List<string>();
	public List<string> inv_names = new List<string>();
	public List<string> inv_int = new List<string>();
	public List<string> inv_str = new List<string>();
	public List<string> inv_con = new List<string>();
	public List<string> inv_rarity = new List<string>();
	
	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
		
		for (int i = 0; i < GameControl.control.inventory.Count; i++){
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
	
	void OnMouseDown () {
		if (!GameControl.control.pause){
			GameControl.control.pause = true;
			GameControl.control.Save();
			Debug.Log("SAVING ON COW CLICK");
		}
	}
	
	void OnGUI () {

		float rx = Screen.width / GameControl.control.native_width;
		float ry = Screen.height / GameControl.control.native_height;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));

		if (GameControl.control.pause){
			GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
			float width = Screen.width - 100;
			float height = Screen.height - 100;
			// Create one Group to contain both images
			// Adjust the first 2 coordinates to place it somewhere else on-screen
			GUI.BeginGroup(new Rect (50, 50, width, height)); // left, top, width, height
				// Draw the background image
				GUI.DrawTexture(new Rect (0, 0, width, height), menu);
				GUI.Label(new Rect(10, 280, 100, 100), "Inventory:", GameControl.control.text);
				GUI.Label(new Rect(10, 280, 100, 100), "\t    " + (GameControl.control.inventory.Count - nullItems), GameControl.control.text);
				GUI.BeginGroup(new Rect(10, 310, 200, 150));
					int i = 0;
					for (int y = 0; y < 3; y++){
						for (int x = 0; x < 4; x++){
							GUI.BeginGroup(new Rect(0 + (x * 50), 0 + (y * 50), 50, 50));
								Texture image = (Texture)Resources.Load("items/textures/" + inv_names[i], typeof(Texture));
								//Texture image = (Texture)Resources.Load(inv_names[i++], typeof(Texture));
								GUI.DrawTexture(new Rect(0, 0, 50, 50), image);
								if (GameControl.control.inventory[i] != "empty\n0\n0\n0\ncommon"){
									if (GUI.Button(new Rect(30, 5, 15, 15), closeButton, GUIStyle.none)){
										if (!shop){
											GameControl.control.inventory.RemoveAt(i);
											GameControl.control.inventory.Add("empty\n0\n0\n0\ncommon");
										}
									}
									if (GUI.Button(new Rect(0, 0, 50, 50), emptyTexture, GUIStyle.none)){
										if (!shop){
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
								}
							GUI.EndGroup();
							i++;
						}
					}
				GUI.EndGroup();
				if (selected1 != -1) GUI.DrawTexture(new Rect(5 + (selected1Pos.x * 50), 305 + (selected1Pos.y * 50), 60, 60), (inv_rarity[selected1] == "winter" || inv_rarity[selected1] == "rare")? switch3 : ((inv_rarity[selected1] == "uncommon")? switch2 : switch1));
			// End both Groups
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height)); // left, top, width, height
				bool hapDif = (GameControl.control.happiness < GameControl.control.happinessLose);
				//Trough trough_obj = GameControl.control.trough;
				float troughExp = GameControl.control.troughExp;
				float troughMaxExp = GameControl.control.troughMaxExp;
				
				if (GameControl.control.happiness < 0.1f){
					GUI.Label(new Rect(60, 110, 100, 100), "Happiness:", GameControl.control.text);
					GUI.Label(new Rect(60, 110, 100, 100), "\t    " + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
				} else if (hapDif){
					GUI.Label(new Rect(60, 110, 100, 100), "Happiness:", GameControl.control.text);
					GUI.Label(new Rect(60, 100, 100, 100), "\t    " + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
					GUI.Label(new Rect(60, 120, 100, 100), "\t    (-" + GameControl.control.happiness.ToString("F1") + "/5s)", GameControl.control.text);
				} else {
					GUI.Label(new Rect(60, 110, 100, 100), "Happiness:", GameControl.control.text);
					GUI.Label(new Rect(60, 100, 100, 100), "\t    " + GameControl.control.happiness.ToString("F1") + " / " + GameControl.control.happinessMax.ToString(), GameControl.control.text);
					GUI.Label(new Rect(60, 120, 100, 100), "\t    (-" + GameControl.control.happinessLose.ToString("F1") + "/5s)", GameControl.control.text);
				}
				
				GUI.Label(new Rect(60, 145, 100, 100), "Money:", GameControl.control.text);
				GUI.Label(new Rect(60, 145, 100, 100), "\t    $" + GameControl.control.money, GameControl.control.text);
				
				GUI.Label(new Rect(60, 165, 100, 100), "Experience:", GameControl.control.text);
				GUI.Label(new Rect(60, 165, 100, 100), "\t    " + GameControl.control.exp.ToString(), GameControl.control.text);
				
				if (troughExp >= 30.0f){
					int troughHours = (int)Mathf.Floor((troughExp * 2) / 60);
					int troughMinutes = (int)((troughExp * 2) - (60 * troughHours));
					if (troughMinutes > 0){
						GUI.Label(new Rect(60, 200, 100, 100), "Trough:", GameControl.control.text);
						GUI.Label(new Rect(60, 180, 100, 100), "\t    " + troughExp.ToString() + " / " + troughMaxExp.ToString(), GameControl.control.text);
						GUI.Label(new Rect(60, 200, 100, 100), "\t    (" + troughHours.ToString("F0") + " hours and", GameControl.control.text);
						GUI.Label(new Rect(60, 220, 100, 100), "\t    " + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
					} else {
						GUI.Label(new Rect(60, 200, 100, 100), "Trough:", GameControl.control.text);
						GUI.Label(new Rect(60, 190, 100, 100), "\t    " + troughExp.ToString() + " / " + troughMaxExp.ToString(), GameControl.control.text);
						GUI.Label(new Rect(60, 210, 100, 100), "\t    (" + troughHours.ToString("F0") + " hours)", GameControl.control.text);
					}
				} else {
					int troughMinutes = (int)(troughExp * 2);
					GUI.Label(new Rect(60, 200, 100, 100), "Trough:", GameControl.control.text);
					GUI.Label(new Rect(60, 190, 100, 100), "\t    " + troughExp.ToString() + " / " + troughMaxExp.ToString(), GameControl.control.text);
					GUI.Label(new Rect(60, 210, 100, 100), "\t    (" + troughMinutes.ToString("F0") + " minutes)", GameControl.control.text);
				}
				
				int addativeStr = (GameControl.control.level >= 5)? int.Parse(inv_str[0]) + int.Parse(inv_str[1]) + int.Parse(inv_str[2]) : 0;
				int addativeCon = (GameControl.control.level >= 5)? int.Parse(inv_con[0]) + int.Parse(inv_con[1]) + int.Parse(inv_con[2]) : 0;
				int addativeInt = (GameControl.control.level >= 5)? int.Parse(inv_int[0]) + int.Parse(inv_int[1]) + int.Parse(inv_int[2]) : 0;
				if (addativeStr == 0){
					GUI.Label(new Rect(60, 240, 100, 100), "Strength:", GameControl.control.text);
					GUI.Label(new Rect(60, 240, 100, 100), "\t    " + GameControl.control.strength.ToString(), GameControl.control.text);
				} else {
					GUI.Label(new Rect(60, 240, 100, 100), "Strength:", GameControl.control.text);
					GUI.Label(new Rect(60, 240, 100, 100), "\t    " + GameControl.control.newStrength.ToString() + "(" + GameControl.control.strength.ToString() + ((addativeStr < 0)? "":"+") + (int.Parse(inv_str[0]) + int.Parse(inv_str[1]) + int.Parse(inv_str[2])).ToString() + ")", GameControl.control.text);
				}
				if (addativeCon == 0){
					GUI.Label(new Rect(60, 270, 100, 100), "Constitution:", GameControl.control.text);
					GUI.Label(new Rect(60, 270, 100, 100), "\t    " + GameControl.control.constitution.ToString(), GameControl.control.text);
				} else {
					GUI.Label(new Rect(60, 270, 100, 100), "Constitution:", GameControl.control.text);
					GUI.Label(new Rect(60, 270, 100, 100), "\t    " + GameControl.control.newConstitution.ToString() + "(" + GameControl.control.constitution.ToString() + ((addativeCon < 0)? "":"+") + (int.Parse(inv_con[0]) + int.Parse(inv_con[1]) + int.Parse(inv_con[2])).ToString() + ")", GameControl.control.text);
				}
				if (addativeInt == 0){
					GUI.Label(new Rect(60, 300, 100, 100), "Intelligence:", GameControl.control.text);
					GUI.Label(new Rect(60, 300, 100, 100), "\t    " + GameControl.control.intelligence.ToString(), GameControl.control.text);
				} else {
					GUI.Label(new Rect(60, 300, 100, 100), "Intelligence:", GameControl.control.text);
					GUI.Label(new Rect(60, 300, 100, 100), "\t    " + GameControl.control.newIntelligence.ToString() + "(" + GameControl.control.intelligence.ToString() + ((addativeInt < 0)? "":"+") + (int.Parse(inv_int[0]) + int.Parse(inv_int[1]) + int.Parse(inv_int[2])).ToString() + ")", GameControl.control.text);
				}
			GUI.EndGroup ();
			
			GUI.BeginGroup(new Rect (50, 50, width, height));
				if (shop){
					GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.95f);
					GUI.DrawTexture(new Rect(0, 0, width, height), menu);
					GUI.Label(new Rect(120, 15, 100, 100), "Money: $" + GameControl.control.money, GameControl.control.text);
					
					GUI.DrawTexture(new Rect(10, 55, 215, 30), blankButton); // Trough upgrade
					GUI.Label(new Rect(15, 60, 100, 100), "Trough Max +25, $500", GameControl.control.text); // Trough upgrade
					if (GUI.Button(new Rect(10, 55, 215, 30), emptyTexture, GUIStyle.none)){ // Trough upgrade
						if (GameControl.control.money > 500){
							GameControl.control.troughMaxExp += 25.0f;
							GameControl.control.money -= 500;
						}
					}
				}
				
				if (GUI.Button(new Rect(width - 50, 10, 30, 30), closeButton, GUIStyle.none)){
					GameControl.control.pause = false;
					shop = false;
				}
				
				if (GUI.Button(new Rect(20, 10, 90, 30), shopButton, GUIStyle.none)){
					shop = !shop;
				}
			
				GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				if (!shop && selected1 != -1){
					GUI.DrawTexture(new Rect(25 + (selected1Pos.x * 50), 270 + (selected1Pos.y * 50), 70, 70), statPopup);
					GUI.Label(new Rect(30 + (selected1Pos.x * 50), 275 + (selected1Pos.y * 50), 70, 70), "Str:", GameControl.control.stats);
					GUI.Label(new Rect(30 + (selected1Pos.x * 50), 275 + (selected1Pos.y * 50), 70, 70), "          " + inv_str[selected1], GameControl.control.stats);
					GUI.Label(new Rect(30 + (selected1Pos.x * 50), 295 + (selected1Pos.y * 50), 70, 70), "Con:", GameControl.control.stats);
					GUI.Label(new Rect(30 + (selected1Pos.x * 50), 295 + (selected1Pos.y * 50), 70, 70), "          " + inv_con[selected1], GameControl.control.stats);
					GUI.Label(new Rect(30 + (selected1Pos.x * 50), 315 + (selected1Pos.y * 50), 70, 70), "Int:", GameControl.control.stats);
					GUI.Label(new Rect(30 + (selected1Pos.x * 50), 315 + (selected1Pos.y * 50), 70, 70), "          " + inv_int[selected1], GameControl.control.stats);
				}
			GUI.EndGroup();
		}
	}
}