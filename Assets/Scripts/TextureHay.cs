using UnityEngine;
using System.Collections;

public class TextureHay : MonoBehaviour {

	public Sprite tex1;
	public Sprite tex2;
	public Sprite tex3;

	// Use this for initialization
	void Start () {
		int tex = Random.Range(0, 30);
		if (tex >= 0 && tex <= 10){
			gameObject.GetComponent<SpriteRenderer>().sprite = tex1;
		} else if (tex >= 11 && tex <= 20){
			gameObject.GetComponent<SpriteRenderer>().sprite = tex2;
		} else if (tex >= 21 && tex <= 30){
			gameObject.GetComponent<SpriteRenderer>().sprite = tex3;
		} else {
			gameObject.GetComponent<SpriteRenderer>().sprite = tex2;
		}
	}
}
