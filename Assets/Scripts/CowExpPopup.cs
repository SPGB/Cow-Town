using UnityEngine;
using System.Collections;

public class CowExpPopup : MonoBehaviour {

	private float die;
	public string val = "+1";
	public float offset_y = -9.5f;
	public float offset_x = -1f;
	private GUIStyle style;

	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		style.fontSize = 20;
		//style.fontStyle = FontStyle.Bold;
	}
	
	// Update is called once per frame
	void Update () {
		if (die >= 60){
			Destroy(gameObject);
		}
		Vector3 temp = new Vector3(0f,-0.02f,0);
		transform.position += temp; 
		die++;
	}
	void OnGUI () {
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.BeginGroup (new Rect (pos.x + offset_x, pos.y + offset_y, 50, 30)); // left, top, width, height

		DrawOutline(new Rect (0, 0, 100, 100), val, 2, style);

		style.normal.textColor = new Color(255,255,255,1); 
		GUI.Label (new Rect (0, 0, 100, 100), val, style);
		GUI.EndGroup ();
	}

	void DrawOutline(Rect r,string t,int strength,GUIStyle style){
		GUI.color=new Color(0,0,0,1);
		int i;
		for (i=-strength;i<=strength;i++){
			GUI.Label(new Rect(r.x-strength,r.y+i,r.width,r.height),t,style);
			GUI.Label(new Rect(r.x+strength,r.y+i,r.width,r.height),t,style);
		}for (i=-strength+1;i<=strength-1;i++){
			GUI.Label(new Rect(r.x+i,r.y-strength,r.width,r.height),t,style);
			GUI.Label(new Rect(r.x+i,r.y+strength,r.width,r.height),t,style);
		}
		GUI.color=new Color(1,1,1,1);
	}
}
