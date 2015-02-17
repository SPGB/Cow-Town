using UnityEngine;
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
	
	public float troughCurExp = 0.0f;
	public float troughMaxExp = 50.0f;
	
	public float totalHay = 0.0f;
	public float totalSpecial = 0.0f;
	
	public GameObject haySelected;
	
	private bool saveMod = false;
	
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
	}
	
	void Update () {
		if (exp % 10 == 0){
			if (!saveMod){
				Save();
				Load();
				saveMod = true;
			}
		} else {
			saveMod = false;
		}
	}
	
	
	void OnDestroy () {
		Save();
	}
	
	void OnApplicationPause () {
		Save();
	}
	void OnApplicationFocus (bool status) {
		if (status == false){
			Load();
		} else if (status == true){
			Save();
		}
	}
	void OnApplicationQuit () {
		Save();
	}
	
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
	
	public float troughCurExp;
	public float troughMaxExp;
	
	public float totalHay;
	public float totalSpecial;
	
	public DateTime saveTime;
	
}