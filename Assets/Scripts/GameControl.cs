using UnityEngine;
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
	public float exp = 0.0f;
	public float expExpected = 0.0f;
	public float level = 1.0f;
	
	public float happiness = 0.0f;
	public float happinessExpected = 0.0f;
	
	public float cowStrength = 10.0f;
	public float cowConstitution = 10.0f;
	public float happinessLose = 1.0f;
	public float cowIntelligence = 10.0f;
	public float happinessMax = 100.0f;
	public List<string> inventory = new List<string>();

	public Trough trough;
	public Cow cow;
	public DateTime cowBorn = DateTime.Now;
	public TimeSpan cowAge;
	public bool isBorn = false;
	public float troughCurExp = 0.0f;
	public float troughMaxExp = 50.0f;
	public float troughPos;
	
	public float totalHay = 0.0f;
	public float totalSpecial = 0.0f;
	
	public GameObject haySelected;
	
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
		
		Load();
		Debug.Log("LOAD ON START");
		updateTime1 = DateTime.Now;
		
		happinessExpected = 0.0f;
		expExpected = 0.0f;
		
		screenSizeX1 = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		screenSizeX2 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		screenSizeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight + 100, 0));
	}
	
	void Update () {
		if (!cow) {
			cow = GameObject.Find("cow").GetComponent<Cow>();
			Load();
		}
		if (!trough) {
			trough = GameObject.Find("trough").GetComponent<Trough>();
			Load();
		}
		if (!pushNoti) pushNoti = GetComponent<PushNotificationsAndroid>();
		
		if (pause){
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	
		if (Input.GetKey(KeyCode.Escape)){
			Save();
			if (Application.loadedLevelName == "barn"){
				Application.LoadLevel("title");
			} else {
				Application.Quit();
			}
		}
		if (Input.GetKey(KeyCode.Menu)){
			Save();
			pause = !pause;
		}

		updateTime2 = DateTime.Now;
		updateTimeSpan = updateTime2 - updateTime1;
		if ((int)updateTimeSpan.TotalMinutes >= 1){
			Save();
			Debug.Log("SAVE ON UPDATE");
			updateTime1 = DateTime.Now;
		}
		/*
			if (troughCurExp > troughMaxExp){
				troughCurExp = troughMaxExp;
			}
			if (troughCurExp < 0.0f){
				troughCurExp = 0.0f;
			}
		*/
		if (happiness > happinessMax){
			happiness = happinessMax;
		}
		if (happiness < 0.0f){
			happiness = 0.0f;
		}
		
		if (cow) {
			if (cow.newConstitution <= 27){
				happinessLose = 1.0f - (cow.newConstitution / 30);
			} else if (!(cow.newConstitution <= 27)) {
				happinessLose = 0.1f;
			}
			
			happinessMax = 100.0f + (Mathf.Floor(cow.newIntelligence / 2));
			
			cowBorn = cow.born;
			inventory = cow.inventory;
			cowStrength = cow.strength;
			cowConstitution = cow.constitution;
			cowIntelligence = cow.intelligence;
		}
		
		if (trough){
			troughCurExp = trough.get_exp();
			troughMaxExp = trough.get_max_exp();
			troughPos = trough.get_xpos();
		}
	}
	
	void OnDestroy () {
		Save();
		Debug.Log("SAVE ON DESTROY");
	}
	
	#if !(UNITY_STANDALONE || UNITY_EDITOR)
	void OnApplicationPause (bool paused) {
		if (paused){
			pushNoti.scheduleLocalNotification("Your cow is hungry!", (int)(5 * (happiness / (1 - (cowConstitution / 30)))));
			pushNoti.scheduleLocalNotification("Your trough is empty!", (int)(60 * (troughCurExp * 2)));
		} else {
			pushNoti.clearLocalNotifications();
		}
	}
	#endif

	public void update_camera() {
		screenSizeX1 = Camera.main.ScreenToWorldPoint (new Vector3 (50, 0, 0));
		screenSizeX2 = Camera.main.ScreenToWorldPoint (new Vector3 (Camera.main.pixelWidth - 50, 0, 0));
		screenSizeY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Camera.main.pixelHeight + 100, 0));
	}
	
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		PlayerData data = new PlayerData();
		
		data.troughPos = troughPos;
		
		data.cowBorn = cowBorn;
		data.isBorn = isBorn;
		
		data.exp = exp;

		data.troughCurExp = troughCurExp;
		data.troughMaxExp = troughMaxExp;
		data.expExpected = expExpected;
		data.level = level;
		
		data.happiness = happiness;
		data.happinessExpected = happinessExpected;
		
		data.cowStrength = cowStrength;
		data.cowConstitution = cowConstitution;
		data.cowIntelligence = cowIntelligence;
		
		data.totalHay = totalHay;
		data.totalSpecial = totalSpecial;
		
		data.statsRandomized = statsRandomized;
		data.numberOfCowsBred = numberOfCowsBred;
		
		data.inventory = inventory;
		
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
			
			isBorn = data.isBorn;
			
			if (cowBorn != data.cowBorn) {
				cowBorn = data.cowBorn;
			}
			
			exp = data.exp;

			expExpected = data.expExpected;
			level = data.level;
			
			happiness = data.happiness;
			happinessExpected = data.happinessExpected;
			
			cowStrength = data.cowStrength;
			cowConstitution = data.cowConstitution;
			cowIntelligence = data.cowIntelligence;
			
			totalHay = data.totalHay;
			totalSpecial = data.totalSpecial;
			
			statsRandomized = data.statsRandomized;
			numberOfCowsBred = data.numberOfCowsBred;
			
			inventory = data.inventory;
			
			DateTime loadTime = DateTime.Now;
			TimeSpan interval = loadTime - data.saveTime;
			int hayUsed = ((int)interval.TotalMinutes) / 2;

			if (trough) {
				trough.set_xpos(data.troughPos);
				trough.set_max_exp(data.troughMaxExp);
				trough.set_exp(data.troughCurExp);
				float current_exp = trough.get_exp();
				for (int i = 0; i < hayUsed; i++){
					if (current_exp == 0){
						break;
					}
					exp++;
					current_exp--;
				}
				trough.set_exp(current_exp);
			}
			
			if (cow){
				if (inventory.Count < 1){
					inventory.Add("hat_afro\n0\n1\n0\nrare");
					inventory.Add("hat_horns\n0\n3\n0\nrare");
					inventory.Add("armor_steel\n-1\n1\n3\nrare");
					inventory.Add("hat_winter\n2\n0\n2\nwinter");
					inventory.Add("cloak_designer\n2\n2\n2\nunique");
					inventory.Add("hat_birthday\n0\n0\n3\nunique");
					inventory.Add("accessory_lei\n0\n0\n1\nuncommon");
					inventory.Add("accessory_pipe\n1\n0\n0\ncommon");
				}
				string oldFormat = "yyyy##MM##dd HH*mm*ss";
				string oldTime = "1800##01##01 00*00*00";
				DateTime old = DateTime.ParseExact(oldTime, oldFormat, null);
				DateTime now = DateTime.Now;
				TimeSpan diff = now - old;
				if (diff.TotalDays > 365){
					cowBorn = DateTime.Now;
				}
				
				if (!isBorn){
					cowBorn = DateTime.Now;
					cow.born = cowBorn;
					isBorn = true;
					Debug.Log("Cow born at: " + cowBorn.Hour + ":" + cowBorn.Minute + " on " + cowBorn.Day + "/" + cowBorn.Month + "/" + cowBorn.Year);
				} else {
					cow.born = cowBorn;
					cow.strength = cowStrength;
					cow.constitution = cowConstitution;
					cow.intelligence = cowIntelligence;
					cow.inventory = inventory;
					Debug.Log("Cow born at: " + cowBorn.Hour + ":" + cowBorn.Minute + " on " + cowBorn.Day + "/" + cowBorn.Month + "/" + cowBorn.Year);
				}
			}
		}
	}
	
	public void Reset(){
		cowBorn = DateTime.Now;
		isBorn = false;
		
		troughPos = 0.0f;
		
		exp = 0.0f;
		expExpected = 0.0f;
		level = 1.0f;
		
		happiness = 0.0f;
		happinessExpected = 0.0f;
		
		cowStrength = 10.0f;
		cowConstitution = 10.0f;
		cowIntelligence = 10.0f;
		
		troughCurExp = 0.0f;
		troughMaxExp = 50.0f;
		
		totalHay = 0.0f;
		totalSpecial = 0.0f;
		
		statsRandomized = false;
		numberOfCowsBred = 0;
		
		inventory = new List<string>();
	}
}

[Serializable]
class PlayerData{

	public DateTime cowBorn;
	public bool isBorn;

	public float troughPos;

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
	
	public List<string> inventory = new List<string>();
	
	public DateTime saveTime;
	
}