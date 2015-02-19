using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	
	public static GameControl control;
	
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
	
	public float troughCurExp = 0.0f;
	public float troughMaxExp = 50.0f;
	
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
		
		exp = 0.0f;
		troughCurExp = 0.0f;
		totalHay = 0.0f;
		totalSpecial = 0.0f;
		
		happinessExpected = 0.0f;
		expExpected = 0.0f;
		statMin = 10 + numberOfCowsBred;
		statMax = 18 + numberOfCowsBred;
		
		if (!statsRandomized){
			randomizeStats(statMin, statMax, statMin, statMax, statMin, statMax);
			statsRandomized = true;
		}
	}
	
	void Update () {
		//if (exp % 10 == 0){
		//	if (!saveModExp){
		//		Save();
		//		Load();
		//		saveModExp = true;
		//	}
		//} else {
		//	saveModExp = false;
		//}
		
		updateTime2 = DateTime.Now;
		updateTimeSpan = updateTime2 - updateTime1;
		if ((int)updateTimeSpan.TotalMinutes >= 1){
			Save();
			Debug.Log("SAVE ON UPDATE");
			updateTime1 = DateTime.Now;
		}
		
		if (troughCurExp > troughMaxExp){
			troughCurExp = troughMaxExp;
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
	
#if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8
	//void OnApplicationPause () {
	//	Save();
	//	Debug.Log("SAVE ON APP PAUSE");
	//}
	void OnApplicationFocus (bool status) {
		if (status == true){
			Load();
			Debug.Log("LOAD ON APP GAIN FOCUS");
		} else if (status == false){
			Save();
			Debug.Log("SAVE ON APP LOSE FOCUS");
		}
	}
	void OnApplicationQuit () {
		Save();
		Debug.Log("SAVE ON APP QUIT");
	}
#endif
	
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		PlayerData data = new PlayerData();
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
			troughCurExp = data.troughCurExp;
			troughMaxExp = data.troughMaxExp;
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
			
			for (int i = 0; i < hayUsed; i++){
				if (troughCurExp > 0){
					exp++;
					troughCurExp--;
				} else {
					break;
				}
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