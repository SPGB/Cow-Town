using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	
	public static GameControl control;
	
	public bool pause = false;
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

	public Trough trough;
	//public float troughCurExp = 0.0f;
	//public float troughMaxExp = 50.0f;
	
	public float totalHay = 0.0f;
	public float totalSpecial = 0.0f;
	
	public GameObject haySelected;
	
	public bool statsRandomized = false;
	public int numberOfCowsBred = 0;
	public int statMin = 10;
	public int statMax = 18;
	
	//private bool saveModExp = false;
	
	private DateTime updateTime1;
	private DateTime updateTime2;
	private TimeSpan updateTimeSpan;

	public Vector3 screenSizeX1;
	public Vector3 screenSizeX2;
	public Vector3 screenSizeY;

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
		Load();
		Debug.Log("LOAD ON START");
		updateTime1 = DateTime.Now;
		
		happinessExpected = 0.0f;
		expExpected = 0.0f;
		statMin = 10 + numberOfCowsBred;
		statMax = 18 + numberOfCowsBred;

		if (!statsRandomized){
			randomizeStats(statMin, statMax, statMin, statMax, statMin, statMax);
			statsRandomized = true;
		}

		screenSizeX1 = Camera.main.ScreenToWorldPoint(new Vector3(50, 0, 0));
		screenSizeX2 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 50, 0, 0));
		screenSizeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight + 100, 0));
	}
	
	void Update () {
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
		
		if (cowConstitution <= 27){
			happinessLose = 1.0f - (cowConstitution / 30);
		} else if (!(cowConstitution <= 27)) {
			happinessLose = 0.1f;
		}
		
		happinessMax = 100.0f + (Mathf.Floor(cowIntelligence / 2));
	}
	
	public void randomizeStats(int strMin, int strMax, int conMin, int conMax, int intMin, int intMax){
		GameControl.control.cowStrength = float.Parse(UnityEngine.Random.Range(strMin, strMax).ToString("F1"));
		//Debug.Log("Str: " + GameControl.control.cowStrength);
		GameControl.control.cowConstitution = float.Parse(UnityEngine.Random.Range(conMin, conMax).ToString("F1"));
		//Debug.Log("Con: " + GameControl.control.cowConstitution);
		GameControl.control.cowIntelligence = float.Parse(UnityEngine.Random.Range(intMin, intMax).ToString("F1"));
		//Debug.Log("Int: " + GameControl.control.cowIntelligence);
	}
	
	void OnDestroy () {
		Save();
		Debug.Log("SAVE ON DESTROY");
	}

	public void update_camera() {
		screenSizeX1 = Camera.main.ScreenToWorldPoint (new Vector3 (50, 0, 0));
		screenSizeX2 = Camera.main.ScreenToWorldPoint (new Vector3 (Camera.main.pixelWidth - 50, 0, 0));
		screenSizeY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Camera.main.pixelHeight + 100, 0));
	}
	
	public void Save(){
		if (!trough) {
			Debug.Log ("no trough, not saving");
			return;
		}
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		PlayerData data = new PlayerData();
		data.exp = exp;

		data.troughCurExp = trough.get_exp();
		data.troughMaxExp = trough.get_max_exp();
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
			
			DateTime loadTime = DateTime.Now;
			TimeSpan interval = loadTime - data.saveTime;
			int hayUsed = ((int)interval.TotalMinutes) / 2;

			if (trough) {
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

		}
	}
}

[Serializable]
class PlayerData{

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
	
	public DateTime saveTime;
	
}