using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wc_manager : MonoBehaviour {
	public Text display;
	public Text user;

	TextAsset txt;
	string[] dict;

	int charaCount;
	int maxLineChara = 66;
	int maxChara = 1254;

	string[] lines = new string[19];
	string[] displayed = new string[19];

	string input;

	float time;
	float interval = 0.25f; //interval to change words at
	int count; //how far the words are going

	/*
	 * box of 600x400 fits 1254 chara at 14pt font in Space Mono
	 * 66 chara per line, 19 lines
	 * 
	 */

	// Use this for initialization
	void Start () {
		txt = (TextAsset)Resources.Load("usa");
		dict = txt.text.Split("\n"[0]);
//		charaCount = 0;
		count = 0;
		input = "";
		lines = new string[19];
		displayed = new string[19];
//		int i = 0;
		for (int i = 0; i < lines.Length; i++) {
			lines [i] = "";
//			lines[i] = fillLine (i);
			for (int j = 0; j < 100; j++) { //just fill each line with 100 words at first :-P UNELEGANT, WHATEVER, I RATHER USE A WHILE LOOP
				int rand = Random.Range (0, dict.Length);
				lines[i] += dict [rand];
			}
			displayed [i] = lines [i].Substring (0, maxLineChara);
		}
		displayText ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 26; i++) {
			if (Input.GetKeyDown (KeyCode.A + i)) {
				if (input.Length < 17) {
					input += (char)(97 + i);
					user.text = input;
				} else {
					//TODO add feedback to show there's too many characters
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			checkMatch ();
		}

		time += Time.deltaTime;
		if (time > interval) {
			time = 0;
			count++;
			moveText();
		}
	}

	void checkMatch(){
		for (int i = 0; i < displayed.Length; i++) {
			if (displayed[i].IndexOf(input)>-1){
				string temp = displayed [i].Substring (0, displayed [i].IndexOf (input));
				temp += input.ToUpper ();
				temp += displayed [i].Substring (temp.Length, maxLineChara - temp.Length);
				displayed [i] = temp;
				Debug.Log ("got it");
				//TODO add actual feedback to show you got the word
				displayText ();
				break;
			}
		}
		input = "";
		user.text = input;
	}

	void moveText(){
		//move stuff
		for (int i = 0; i < displayed.Length; i++) {
			displayed [i] = lines [i].Substring(count,maxLineChara);
		}
		displayText ();
	}

	void displayText(){
		display.text = "";
		for (int i = 0; i < displayed.Length; i++) {
			display.text += displayed [i] + "\n";
		}
	}

	//not used, old shit, didn't work, cried about it
	string fillLine(int i){
		while (charaCount <= maxLineChara) {
			int rand = Random.Range (0, dict.Length);
			lines[i] += dict [rand];
			charaCount += dict [rand].Length;
			Debug.Log (dict [rand] + " "+charaCount);
		}
		Debug.Log (lines[i].Length);
		lines[i] = lines[i].Substring (0, maxLineChara);
		return lines[i];
	}
}
