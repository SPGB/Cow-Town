using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnItems : MonoBehaviour {

	public string COMMON = "";
	public GameObject common1;
	public GameObject common2;
	public GameObject common3;
	public GameObject common4;
	public GameObject common5;
	public GameObject common6;
	private List<GameObject> commonItems = new List<GameObject>();
	public string UNCOMMON = "";
	public GameObject uncommon1;
	public GameObject uncommon2;
	public GameObject uncommon3;
	public GameObject uncommon4;
	public GameObject uncommon5;
	public GameObject uncommon6;
	public GameObject uncommon7;
	public GameObject uncommon8;
	public GameObject uncommon9;
	public GameObject uncommon10;
	public GameObject uncommon11;
	private List<GameObject> uncommonItems = new List<GameObject>();
	public string RARE = "";
	public GameObject rare1;
	public GameObject rare2;
	public GameObject rare3;
	public GameObject rare4;
	public GameObject rare5;
	public GameObject rare6;
	public GameObject rare7;
	public GameObject rare8;
	public GameObject rare9;
	public GameObject rare10;
	public GameObject rare11;
	public GameObject rare12;
	public GameObject rare13;
	public GameObject rare14;
	public GameObject rare15;
	public GameObject rare16;
	public GameObject rare17;
	public GameObject rare18;
	public GameObject rare19;
	public GameObject rare20;
	public GameObject rare21;
	public GameObject rare22;
	public GameObject rare23;
	public GameObject rare24;
	public GameObject rare25;
	public GameObject rare26;
	public GameObject rare27;
	private List<GameObject> rareItems = new List<GameObject>();
	
	private string itemStringSpawn;
	private List<string> commonStrings = new List<string>();
	private List<string> uncommonStrings = new List<string>();
	private List<string> rareStrings = new List<string>();
	
	public Vector3 chanceRange;
	
	public float chanceX;
	public float chanceY;
	public float chance;
	public float happen;
	
	public bool spawning;
	
	private int regulator;
	public int regulatorCap = 5;

	// Use this for initialization
	void Start () {
		commonItems.Add(common1);
		commonStrings.Add("hat_basic\n0\n0\n0\ncommon");
		commonItems.Add(common2);
		commonStrings.Add("coat_basic\n0\n0\n0\ncommon");
		commonItems.Add(common3);
		commonStrings.Add("accessory_pipe\n1\n0\n0\ncommon");
		commonItems.Add(common4);
		commonStrings.Add("shoes_basic\n0\n0\n0\ncommon");
		commonItems.Add(common5);
		commonStrings.Add("hat_deerstalker\n0\n0\n1\ncommon");
		commonItems.Add(common6);
		commonStrings.Add("shoes_cowboy\n0\n0\n0\ncommon");
		uncommonItems.Add(uncommon1);
		uncommonStrings.Add("hat_beerhat\n0\n0\n2\nuncommon");
		uncommonItems.Add(uncommon2);
		uncommonStrings.Add("accessory_lei\n0\n0\n1\nuncommon");
		uncommonItems.Add(uncommon3);
		uncommonStrings.Add("hat_fedora\n-1\n0\n0\nuncommon");
		uncommonItems.Add(uncommon4);
		uncommonStrings.Add("accessory_monocle\n2\n0\n0\nuncommon");
		uncommonItems.Add(uncommon5);
		uncommonStrings.Add("skirt_grass\n1\n0\n0\nuncommon");
		uncommonItems.Add(uncommon6);
		uncommonStrings.Add("accessory_coconut\n0\n1\n0\nuncommon");
		uncommonItems.Add(uncommon7);
		uncommonStrings.Add("wings_rainbow\n1\n0\n0\nuncommon");
		uncommonItems.Add(uncommon8);
		uncommonStrings.Add("hat_cone\n1\n1\n1\nuncommon");
		uncommonItems.Add(uncommon9);
		uncommonStrings.Add("hat_rainbowafro\n1\n2\n1\nuncommon");
		uncommonItems.Add(uncommon10);
		uncommonStrings.Add("wings_bat\n1\n0\n1\nuncommon");
		uncommonItems.Add(uncommon11);
		uncommonStrings.Add("dress_lace\n1\n1\n1\nuncommon");
		rareItems.Add(rare1);
		rareStrings.Add("hat_afro\n0\n2\n0\nrare");
		rareItems.Add(rare2);
		rareStrings.Add("tutu_pink\n1\n0\n1\nrare");
		rareItems.Add(rare3);
		rareStrings.Add("hat_horns\n0\n3\n0\nrare");
		rareItems.Add(rare4);
		rareStrings.Add("hat_jester\n1\n0\n0\nrare");
		rareItems.Add(rare5);
		rareStrings.Add("accessory_marotte\n1\n0\n0\nrare");
		rareItems.Add(rare6);
		rareStrings.Add("hat_crown\n2\n0\n1\nrare");
		rareItems.Add(rare7);
		rareStrings.Add("hat_goldenafro\n3\n0\n0\nrare");
		rareItems.Add(rare8);
		rareStrings.Add("hat_astronaut\n2\n0\n2\nrare");
		rareItems.Add(rare9);
		rareStrings.Add("suit_astronaut\n2\n0\n2\nrare");
		rareItems.Add(rare10);
		rareStrings.Add("suit_batcow\n2\n1\n1\nrare");
		rareItems.Add(rare11);
		rareStrings.Add("pet_blair\n2\n2\n2\nrare");
		rareItems.Add(rare12);
		rareStrings.Add("mask_cthulhu\n2\n2\n1\nrare");
		rareItems.Add(rare13);
		rareStrings.Add("grabber_dino\n-1\n3.5\n0\nrare");
		rareItems.Add(rare14);
		rareStrings.Add("boots_fauxfur\n0\n3\n1\nrare");
		rareItems.Add(rare15);
		rareStrings.Add("coat_fauxfur\n3\n0\n1\nrare");
		rareItems.Add(rare16);
		rareStrings.Add("pet_shiba\n2\n2\n2\nrare");
		rareItems.Add(rare17);
		rareStrings.Add("hat_wolf\n2\n2\n1\nrare");
		rareItems.Add(rare18);
		rareStrings.Add("hat_mercow\n2\n2\n-1\nrare");
		rareItems.Add(rare19);
		rareStrings.Add("accessory_mercow\n2\n-1\n2\nrare");
		rareItems.Add(rare20);
		rareStrings.Add("tail_mercow\n-1\n2\n2\nrare");
		rareItems.Add(rare21);
		rareStrings.Add("hat_moose\n2\n0\n2\nwinter");
		rareItems.Add(rare22);
		rareStrings.Add("coat_puffy\n0\n2\n2\nwinter");
		rareItems.Add(rare23);
		rareStrings.Add("hat_winter\n2\n0\n2\nwinter");
		rareItems.Add(rare24);
		rareStrings.Add("accessory_winter\n0\n2\n2\nwinter");
		rareItems.Add(rare25);
		rareStrings.Add("shield_steel\n0\n0\n3\nrare");
		rareItems.Add(rare26);
		rareStrings.Add("armor_steel\n-1\n1\n3\nrare");
		rareItems.Add(rare27);
		rareStrings.Add("sword_steel\n-1\n4\n-1\nrare");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0.0f){
			if (regulator >= regulatorCap){
				chanceX = (chanceRange.x);
				chanceY = (chanceRange.y);
				chance = Random.Range(chanceX, chanceY);
				happen = (chanceRange.z);
				if (chance >= (happen - 0.05f) && chance <= (happen + 0.05f)){
					spawning = true;
				}
				regulator = 0;
			}
			regulator++;
		}
		
		if (spawning){
			int rarity = Random.Range(0, 100);
			int i = (rarity >= 92)? ((rarity >=97)? Random.Range(1, rareItems.Count) : Random.Range(1, uncommonItems.Count)) : Random.Range(1, commonItems.Count);
			
			GameObject new_object = Instantiate(((rarity >= 92)? ((rarity >=97)? rareItems[i-1] : uncommonItems[i-1]) : commonItems[i-1]), new Vector3(Random.Range(GameControl.control.screenSizeX1.x, GameControl.control.screenSizeX2.x), GameControl.control.screenSizeY.y, 4.4f), Quaternion.identity) as GameObject;
			
			itemStringSpawn = (rarity >= 92)? ((rarity >=97)? rareStrings[i-1] : uncommonStrings[i-1]) : commonStrings[i-1];
			
			Debug.Log("Spawning item: " + itemStringSpawn);
			
			new_object.AddComponent<BoxCollider>();
			new_object.AddComponent<Rigidbody>();
			new_object.rigidbody.drag = 7;
			new_object.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
			new_object.AddComponent<ItemCollide>();
			new_object.GetComponent<ItemCollide>().popup = GameControl.control.popup;
			new_object.GetComponent<ItemCollide>().itemString = itemStringSpawn;
			new_object.AddComponent<DragItem>();
			new_object.AddComponent<HayFall>();
			
			spawning = false;
		}
	}
}
