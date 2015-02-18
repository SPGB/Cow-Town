﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	
	public static GameControl control;
	
	public float exp = 0.0f;
	public float expExpected = 0.0f;
	public float expReqToLevel = 10.0f;
	public int level = 1;
	
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
		
		if (cowConstitution <= 27){
			happinessLose = 1.0f - (cowConstitution / 30);
		} else if (!(cowConstitution <= 27)) {
			happinessLose = 0.1f;
		}
		
		happinessMax = 100.0f + (Mathf.Floor(cowIntelligence / 2));
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
		data.expReqToLevel = expReqToLevel;
		data.level = level;
		
		data.happiness = happiness;
		data.happinessExpected = happinessExpected;
		
		data.cowStrength = cowStrength;
		data.cowConstitution = cowConstitution;
		data.cowIntelligence = cowIntelligence;
		
		data.totalHay = totalHay;
		data.totalSpecial = totalSpecial;
		
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
			expReqToLevel = data.expReqToLevel;
			level = data.level;
			
			happiness = data.happiness;
			happinessExpected = data.happinessExpected;
			
			cowStrength = data.cowStrength;
			cowConstitution = data.cowConstitution;
			cowIntelligence = data.cowIntelligence;
			
			totalHay = data.totalHay;
			totalSpecial = data.totalSpecial;
			
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
	public float expReqToLevel;
	public int level;
	
	public float happiness;
	public float happinessExpected;
	
	public float cowStrength;
	public float cowConstitution;
	public float cowIntelligence;
	
	public float troughCurExp;
	public float troughMaxExp;
	
	public float totalHay;
	public float totalSpecial;
	
	public DateTime saveTime;
	
}