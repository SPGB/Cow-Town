using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cow : MonoBehaviour {

	public Texture menu;
	public Texture closeButton;
	public Texture shopButton;
	public Texture buyButton;
	private bool shop;
	
	public DateTime born;
	public DateTime now;
	public TimeSpan age;
	
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
		GameControl.control.Load();
		inventory.Add("coat_basic\n0\n0\n0\ncommon");
		inventory.Add("hat_deerstalker\n1\n0\n0\ncommon");
		inventory.Add("cloak_ofthesun\n2\n2\n2\nunique");
		inventory.Add("pet_blair\n2\n2\n2\nrare");
		inventory.Add("sword_steel\n-1\n4\n-1\nrare");
		inventory.Add("sword_steel\n-1\n4\n-1\nrare");
		inventory.Add("armor_steel\n-1\n1\n3\nrare");
		inventory.Add("armor_steel\n-1\n1\n3\nrare");
		inventory.Add("armor_steel\n-1\n1\n3\nrare");
		inventory.Add("hat_wolf\n2\n2\n1\nrare");
		inventory.Add("coat_puffy\n0\n2\n2\nwinter");
		inventory.Add("hat_rainbowafro\n1\n2\n1\nuncommon");
		
		for (int i = 0; i < 12; i++){
			inv_names.Add("");
			inv_int.Add("");
			inv_str.Add("");
			inv_con.Add("");
			inv_rarity.Add("");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (born != GameControl.control.cowBorn) born = GameControl.control.cowBorn;
		now = DateTime.Now;
		age = now - born;
		GameControl.control.cowAge = age;
		
		if (inventory.Count > 0){
			for (int i = 0; i < inventory.Count; i++){
				string[] split = inventory[i].Split();
				inv_names[i] = split[0].ToString();
				inv_int[i] = split[1].ToString();
				inv_str[i] = split[2].ToString();
				inv_con[i] = split[3].ToString();
				inv_rarity[i] = split[4].ToString();
			}
		}
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
				GUI.Label(new Rect(60, 280, 100, 100), "Inventory:", GameControl.control.text);
				GUI.BeginGroup(new Rect(60, 310, 200, 150));
					int i = 0;
					for (int y = 0; y < 3; y++){
						for (int x = 0; x < 4; x++){
							Texture image = (Texture)Resources.Load(inv_names[i++], typeof(Texture));
							GUI.DrawTexture(new Rect(0 + (x * 50), 0 + (y * 50), 50, 50), image);
						}
					}
				GUI.EndGroup();
			// End both Groups
			GUI.EndGroup ();
			
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
				
				GUI.Label(new Rect(110, 150, 100, 100), "Experience:\t\t\t" + GameControl.control.exp.ToString(), GameControl.control.text);
				
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
				
				GUI.Label(new Rect(110, 240, 100, 100), "Strength:\t\t\t\t" + GameControl.control.cowStrength.ToString(), GameControl.control.text);
				GUI.Label(new Rect(110, 270, 100, 100), "Constitution:\t\t\t" + GameControl.control.cowConstitution.ToString(), GameControl.control.text);
				GUI.Label(new Rect(110, 300, 100, 100), "Intelligence:\t\t\t" + GameControl.control.cowIntelligence.ToString(), GameControl.control.text);
			GUI.EndGroup ();
			
			GUI.BeginGroup(new Rect (50, 50, width, height));
				if (shop){
					GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.95f);
					GUI.DrawTexture(new Rect (0, 0, width, height), menu);
					GUI.Label(new Rect(60, 60, 100, 100), "Upgrades:", GameControl.control.text);
					
					if (GUI.Button(new Rect(180, 55, 90, 30), buyButton, GUIStyle.none)){ // Trough upgrade
						if (GameControl.control.exp > 500){
							GameControl.control.trough.set_max_exp(GameControl.control.trough.get_max_exp() + 25);
							GameControl.control.exp -= 500;
						}
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