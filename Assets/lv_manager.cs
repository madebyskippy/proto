using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lv_manager : MonoBehaviour {

	public GameObject screen;
	public Text p1Input;
	public Text p1Output;
	public Text p2Input;
	public Text p2Output;
	public InputField p1;
	public InputField p2;

	int activePlayer = 1; //using indexing counting, so this is actually player 2

	// Use this for initialization
	void Start () {
		p1.enabled = false;
		activePlayer = 1;
		p2.Select ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void p2Enter(){
		p1Output.text += "them: "+p2.text+"\n";
		p2Output.text += "you: "+p2.text+"\n";
		activePlayer = 0;
		p2.enabled = false;
		p1.enabled = true;
		screen.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		p1.Select ();
	}

	public void p1Enter(){
		string inp = p1.text;
		List<string> words = new List<string> (inp.Split (new string[]{" "},System.StringSplitOptions.RemoveEmptyEntries));
		string output="";
		for (int i = 0; i < words.Count; i++) {
			if (Random.Range(0f,1f) < 0.5f)
				output += words [i] + " ";
		}
		p1Output.text += "you: "+output+"\n";
		p2Output.text += "them: "+output+"\n";
		activePlayer = 1;
		p1.enabled = false;
		p2.enabled = true;
		screen.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, 0);
		p2.Select ();
	}
}
