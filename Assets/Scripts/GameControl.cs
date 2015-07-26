﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	
	public static GameControl control;
	public static PushNotificationsAndroid pushNoti;
	
	public bool pause;
	public String version = "0.01A";
	public bool titleScreen = true;

	public float native_width = 399.0f;
	public float native_height = 638.0f;
	
	public GameObject popup;

	public bool initialRun = true;
	
	public int money = 0;
	public int milk = 0;
	public float exp = 0.0f;
	public float expExpected = 0.0f;
	public float level = 1.0f;
	public float troughExp = 0.0f;
	public float troughMaxExp = 50.0f;
	
	public float happiness = 0.0f;
	public float happinessExpected = 0.0f;
	
	public float happinessLose = 1.0f;
	public float happinessMax = 100.0f;
	public List<string> inventory = new List<string>();
	
	public float strength = 10.0f;
	public float newStrength = 10.0f;
	public float constitution = 10.0f;
	public float newConstitution = 10.0f;
	public float intelligence = 10.0f;
	public float newIntelligence = 10.0f;

	public string cowName = "Betty";
	public string[] defaultNames = new string[5];
	
	private int statMin = 10;
	private int statMax = 18;

	public Trough trough;
	public Cow cow;
	public DateTime cowBorn = DateTime.Now;
	public TimeSpan cowAge;
	public bool isBorn = false;
	public float troughPos;
	
	public float totalHay = 0.0f;
	public float totalSpecial = 0.0f;
	
	public GameObject haySelected;
	public GameObject barHap;
	
	public int numberOfCowsBred = 0;
	public bool statsRandomized;
	
	//private bool saveModExp = false;
	
	private DateTime updateTime1;
	private DateTime updateTime2;
	private TimeSpan updateTimeSpan;

	public Vector3 screenSizeX1;
	public Vector3 screenSizeX2;
	public Vector3 screenSizeY;
	
	public GUIStyle text;
	public GUIStyle stats;
	public GUIStyle cowText;
	public GUIStyle moneyText;
	public GUIStyle nameText;

	public float textMulti = 20;
	public float statTextMulti = 16;
	public float moneyTextMulti = 28;

	public Vector2 buttonSize = new Vector2(128, 64);
	public float templateHeight = 640;
	public float screenMulti;
	
	public bool draggingItem = false;
	public bool shopDragged = false;
	public bool statDragged = false;
	public float shopOffset;
	public float statOffset;
	public float dragStart = 0;
	public float dragDifference = 0;
	
	private float runonce = 0;
	public float spawnMaxTime;
	public float happinessDeclineRate;

	// Use this for initialization
	void Awake () {
		if (control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		} else if (control != this){
			Destroy(gameObject);
		}
	}
	
	void Start () {
		pushNoti = GetComponent<PushNotificationsAndroid>();
		
		text = new GUIStyle();
		text.fontSize = 20;
		text.normal.textColor = Color.black;

		stats = new GUIStyle();
		stats.fontSize = 16;

		cowText = new GUIStyle();
		cowText.fontSize = 20;
		cowText.normal.textColor = Color.white;

		nameText = new GUIStyle();
		nameText.fontSize = 22;
		nameText.normal.textColor = Color.white;

		moneyText = new GUIStyle();
		moneyText.fontSize = 28;
		moneyText.normal.textColor = Color.black;
		
		updateTime1 = DateTime.Now;
		
		happinessExpected = 0.0f;
		expExpected = 0.0f;
		
		screenSizeX1 = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		screenSizeX2 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		screenSizeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight + 100, 0));

		Debug.Log("Screen Size: X: " + Screen.width + ", Y: " + Screen.height);
		
		statMin = 10 + numberOfCowsBred;
		statMax = 18 + numberOfCowsBred;
		if (!statsRandomized){
			randomizeStats(statMin, statMax, statMin, statMax, statMin, statMax);
			statsRandomized = true;
		}

		defaultNames[0] = "Betty";
		defaultNames[1] = "Billy";
		defaultNames[2] = "Bonny";
		defaultNames[3] = "Bessy";
		defaultNames[4] = "Bucky";

		cowName = defaultNames [UnityEngine.Random.Range (0, 4)].Trim();

		cow.setName (cowName);

		statOffset = Screen.width;
		shopOffset = 0 - Screen.width;

		Load ();

	}
	
	void Update () {
		screenMulti = (Screen.height / templateHeight);
		//Debug.Log(screenMulti);

		float moneyTextScale = 28 * screenMulti;
		float textScale = 20 * screenMulti;
		float statScale = 16 * screenMulti;

		text.fontSize = (int)textScale;
		stats.fontSize = (int)statScale;
		cowText.fontSize = (int)textScale;
		moneyText.fontSize = (int)moneyTextScale;

		update_camera ();

		if (!cow && Application.loadedLevelName == "barn") {
			cow = GameObject.Find("cow").GetComponent<Cow>();
			cowBorn = DateTime.Now;
			Load();
			Debug.Log("LOAD ON COW LOAD");
		}
		if (!trough && Application.loadedLevelName == "barn") {
			trough = GameObject.Find("trough").GetComponent<Trough>();
			Load();
			Debug.Log("LOAD ON TROUGH LOAD");
		}
		if (!pushNoti) pushNoti = GetComponent<PushNotificationsAndroid>();
		
		if (pause){
			Time.timeScale = 0.0f;
			gameObject.transform.position = new Vector3(0.0f, 0.0f, 3.0f);
		} else {
			Time.timeScale = 1.0f;
			gameObject.transform.position = new Vector3(0.0f, 0.0f, 6.0f);
		}
	
		if (Input.GetKey(KeyCode.Escape)){
			if (Application.loadedLevelName == "barn"){
				Save();
				Application.LoadLevel("title");
			} else {
				Application.Quit();
			}
		}
		if (Input.GetKeyUp(KeyCode.Menu)){
			if (Application.loadedLevelName == "barn"){
				Save();
				pause = !pause;
			}
		}

		updateTime2 = DateTime.Now;
		updateTimeSpan = updateTime2 - updateTime1;
		if ((int)updateTimeSpan.TotalMinutes >= 1){
			if (Application.loadedLevelName == "barn"){
				Save();
				Load();
				Debug.Log("SAVE/LOAD ON UPDATE");
			}
			updateTime1 = DateTime.Now;
		}
	
		if (happiness > happinessMax){
			happiness = happinessMax;
		}
		if (happiness < 0.0f){
			happiness = 0.0f;
		}
		if (money < 0){
			money = 0;
		}
		
		if (cow) {
		
			if (GameControl.control.level >= 5){
				float additionStr = int.Parse(cow.inv_str[0]) + int.Parse(cow.inv_str[1]) + int.Parse(cow.inv_str[2]);
				newStrength = strength + additionStr;
				
				float additionCon = int.Parse(cow.inv_con[0]) + int.Parse(cow.inv_con[1]) + int.Parse(cow.inv_con[2]);
				newConstitution = constitution + additionCon;
				
				float additionInt = int.Parse(cow.inv_int[0]) + int.Parse(cow.inv_int[1]) + int.Parse(cow.inv_int[2]);
				newIntelligence = intelligence + additionInt;
				
				cow.items.SetActive(true);
			} else {
				cow.items.SetActive(false);
			}
		
			if (newConstitution <= 27){
				happinessLose = 1.0f - (newConstitution / 30);
			} else if (!(newConstitution <= 27)) {
				happinessLose = 0.1f;
			}
			
			happinessMax = 100.0f + (Mathf.Floor(newIntelligence / 2));
			
			DateTime now = DateTime.Now;
			cowAge = now - cowBorn;
			
			for (int c = 0; c < 12; c++){
				if (inventory[c] == "empty\n0\n0\n0\ncommon"){
					inventory.RemoveAt(c);
					inventory.Add("empty\n0\n0\n0\ncommon");
				}
			}

			if (Application.loadedLevelName == "title") titleScreen = true;
			else titleScreen = false;

			if (statOffset < 0) {
				statOffset = 0;
				shopOffset = 0 - (Screen.width * 2);
			} else if (statOffset > Screen.width * 2){
				statOffset = Screen.width * 2;
				shopOffset = 0;
			}
			if (shopOffset > 0) {
				shopOffset = 0;
				statOffset = Screen.width * 2;
			} else if (shopOffset < 0 - (Screen.width * 2)){
				shopOffset = 0 - (Screen.width * 2);
				statOffset = 0;
			}
		}
	}

	void OnMouseDrag(){

		if (runonce > 0){
			runonce = 1;
		} else {
			dragStart = Input.mousePosition.x;
		}
		runonce++;

		if (!draggingItem && !titleScreen) {
			if (!statDragged && !shopDragged){
				if (dragStart <= 0 + (Screen.width / 10) || dragStart >= Screen.width - (Screen.width / 10)){
					pause = true;
					dragDifference = Input.mousePosition.x - dragStart;
				}
			}

			if (statDragged){
				pause = true;
				dragDifference = Input.mousePosition.x - dragStart;

				if (dragDifference < -1) {
					dragDifference = 0;
				}

				statOffset = 0 + dragDifference;
				shopOffset = (0 - (Screen.width * 2)) + dragDifference;

			} else if (shopDragged){
				pause = true;
				dragDifference = Input.mousePosition.x - dragStart;

				if (dragDifference > 1) {
					dragDifference = 0;
				}

				statOffset = (Screen.width * 2) + dragDifference;
				shopOffset = 0 + dragDifference;

			} else {
				statOffset = Screen.width + dragDifference;
				shopOffset = (0 - Screen.width) + dragDifference;
			}
		}
	}

	void OnMouseUp(){

		if (statDragged) {
			if (statOffset > Screen.width / 8) {
				statOffset = Screen.width;
				shopOffset = 0 - Screen.width;
				statDragged = false;
				pause = false;
			} else {
				statOffset = 0;
				shopOffset = 0 - (Screen.width * 2);
				pause = true;
			}
		} else if (shopDragged) {
			if (shopOffset < 0 - (Screen.width / 8)) {
				statOffset = Screen.width;
				shopOffset = 0 - Screen.width;
				shopDragged = false;
				pause = false;
			} else {
				statOffset = Screen.width * 2;
				shopOffset = 0;
				pause = true;
			}
		} else {
			if (shopOffset > 0 - (Screen.width / 2) - (Screen.width / 8)) {
				statOffset = Screen.width * 2;
				shopOffset = 0;
				shopDragged = true;
				Save();
				pause = true;
			} else if (statOffset < (Screen.width / 2) + (Screen.width / 8)) {
				statOffset = 0;
				shopOffset = 0 - (Screen.width * 2);
				statDragged = true;
				Save();
				pause = true;
			} else {
				statOffset = Screen.width;
				shopOffset = 0 - Screen.width;
				statDragged = false;
				shopDragged = false;
				pause = false;
			}
		}

		dragDifference = 0;
		dragStart = 0;
		
		runonce = 0;
	}
	
	void OnDestroy () {
		if (Application.loadedLevelName == "barn"){
			Save();
			Debug.Log("SAVE ON DESTROY");
		}
	}
	
	#if !(UNITY_STANDALONE || UNITY_EDITOR)
	void OnApplicationPause (bool paused) {
		if (paused){
			float happDelay = 5 * (happiness / (1 - (constitution / 30)));
			float troughDelay = 60 * (troughExp * 2);
			pushNoti.scheduleLocalNotification("Your cow is hungry!", (int)( 360 + happDelay));
			pushNoti.scheduleLocalNotification("Your trough is empty!", (int)(360 + troughDelay));
		} else {
			pushNoti.clearLocalNotifications();
		}
	}
	void OnApplicationFocus (bool focused) {
		if (!focused && Application.loadedLevelName == "barn"){
			Save();
			Debug.Log("SAVE ON APP FOCUS");
		} else {
			if (Application.loadedLevelName == "barn"){
				Load();
				Debug.Log("LOAD ON APP FOCUS");
			}
		}
	}
	#endif
	
	void OnApplicationQuit(){
		if (Application.loadedLevelName == "barn"){
			Save();
			Debug.Log("SAVE ON APP QUIT");
		}
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

	public void update_camera() {
		screenSizeX1 = Camera.main.ScreenToWorldPoint (new Vector3 (50, 0, 0));
		screenSizeX2 = Camera.main.ScreenToWorldPoint (new Vector3 (Camera.main.pixelWidth - 50, 0, 0));
		screenSizeY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Camera.main.pixelHeight + 100, 0));
		if (Application.loadedLevelName == "barn") {
			spawnMaxTime = Camera.main.GetComponent<SpawnHay> ().getMaxTime ();
			happinessDeclineRate = GameObject.Find("barHap").GetComponent<Happiness>().getRate();
		}
	}
	
	public void Delete(){
		PlayerPrefs.DeleteAll();
		Reset();
		Save();
		Load();
	}
	
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		PlayerData data = new PlayerData();

		data.initialRun = initialRun;
		
		data.troughPos = troughPos;
		
		data.cowBorn = cowBorn;
		data.isBorn = isBorn;

		data.money = money;
		
		data.exp = exp;

		data.troughCurExp = troughExp;
		data.troughMaxExp = troughMaxExp;
		data.expExpected = expExpected;
		data.level = level;
		
		data.happiness = happiness;
		data.happinessExpected = happinessExpected;
		
		data.cowStrength = strength;
		data.cowConstitution = constitution;
		data.cowIntelligence = intelligence;
		
		data.totalHay = totalHay;
		data.totalSpecial = totalSpecial;
		
		data.statsRandomized = statsRandomized;
		data.numberOfCowsBred = numberOfCowsBred;
		
		data.inventory = inventory;

		data.maxSpawnTime = spawnMaxTime;
		data.happinessDecRate = happinessDeclineRate;
		data.inventoryRows = cow.inventoryRows;

		data.cowName = cow.getName();
		
		data.saveTime = DateTime.Now;
		
		Debug.Log("Saving to... " + Application.persistentDataPath + "/playerInfo.dat" + " at: " + DateTime.Now);
		
		bf.Serialize(file, data);
		file.Close();
	}
	
	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();
			
			Debug.Log("Loading from... " + Application.persistentDataPath + "/playerInfo.dat" + " at: " + DateTime.Now);

			initialRun = data.initialRun;
			
			isBorn = data.isBorn;
			
			cowBorn = data.cowBorn;

			exp = data.exp;
			money = data.money;

			expExpected = data.expExpected;
			level = data.level;
			
			happiness = data.happiness;
			happinessExpected = data.happinessExpected;
			
			strength = data.cowStrength;
			constitution = data.cowConstitution;
			intelligence = data.cowIntelligence;
			
			totalHay = data.totalHay;
			totalSpecial = data.totalSpecial;
			
			statsRandomized = data.statsRandomized;
			numberOfCowsBred = data.numberOfCowsBred;
			
			inventory = data.inventory;
			
			troughMaxExp = data.troughMaxExp;
			troughPos = data.troughPos;

			Camera.main.GetComponent<SpawnHay>().setMaxTime(data.maxSpawnTime);
			GameObject.Find("barHap").GetComponent<Happiness>().setRate(data.happinessDecRate);
			cow.setInventoryRows(data.inventoryRows);
			cow.setName(data.cowName);
			
			DateTime loadTime = DateTime.Now;
			TimeSpan interval = loadTime - data.saveTime;
			int hayUsed = ((int)interval.TotalMinutes) / 2;

			float current_exp = data.troughCurExp;
			for (int i = 0; i < hayUsed; i++){
				if (current_exp <= 0){
					break;
				}
				exp++;
				current_exp--;
			}
			troughExp = current_exp;
			
			if (!data.isBorn){
				cowBorn = DateTime.Now;
				isBorn = true;
			}
			Debug.Log("Cow born at: " + cowBorn.Hour + ":" + cowBorn.Minute + ":" + cowBorn.Second + " on " + cowBorn.Day + "/" + cowBorn.Month + "/" + cowBorn.Year);
		}
	}
	
	public void Reset(){
		cowBorn = DateTime.Now;
		isBorn = false;
		
		troughPos = 0.0f;
		
		money = 0;
		
		exp = 0.0f;
		expExpected = 0.0f;
		level = 1.0f;
		
		happiness = 0.0f;
		happinessExpected = 0.0f;
		
		strength = 10.0f;
		constitution = 10.0f;
		intelligence = 10.0f;
		
		troughExp = 0.0f;
		troughMaxExp = 50.0f;
		
		totalHay = 0.0f;
		totalSpecial = 0.0f;
		
		statsRandomized = false;
		numberOfCowsBred = 0;
		
		inventory.Clear();
		for (int i = 0; i < 12; i++){
			inventory.Add("empty\n0\n0\n0\ncommon");
		}
	}
}

[Serializable]
class PlayerData{

	public bool initialRun;

	public DateTime cowBorn;
	public bool isBorn;

	public string cowName;

	public float troughPos;

	public int money;

	public float exp;
	public float expExpected;
	public float level;
	
	public float happiness;
	public float happinessExpected;
	
	public float cowStrength;
	public float cowConstitution;
	public float cowIntelligence;
	
	public float troughCurExp;
	public float troughMaxExp;
	
	public float totalHay;
	public float totalSpecial;
	
	public bool statsRandomized;
	public int numberOfCowsBred;

	public float maxSpawnTime;
	public int inventoryRows;
	public float happinessDecRate;
	
	public List<string> inventory = new List<string>();
	
	public DateTime saveTime;
	
}