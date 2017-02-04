using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class wc_manager : MonoBehaviour {
	public Text display;
	public Text user;
	public Text UIname;
	public Text UIscore;
	public Text UItime;

	string instructions = "O.K. thank you.\n\nplease type in any words you see on the screen.\npress enter to submit the words.\n\nplease press enter to begin.";
	int instructionsIndex;

	int mode;
	/*
	 * 0=enter name
	 * 1=instructions
	 * 2=gameplay (horizontal)
	 * 3=transition
	 * 4=gameplay (vertical)
	 * 5="processing"
	 * 6=end with scores/data.
	 */

	TextAsset txt;
	string[] dict;

	int charaCount;
	int maxLineChara = 66;
	int maxChara = 1254;

	string[] lines = new string[19];
	string[] linesV = new string[33];
	string[] displayed = new string[19];
	string[] displayedV = new string[33];

	string input;
	string name;

	float time;
	float interval = 0.25f; //interval to change words at
	float totalTime=30f;
	int count; //how far the words are going

	/*
	 * for horizontal:
	 * box of 600x400 fits 1254 chara at 14pt font in Space Mono
	 * 66 chara per line, 19 lines
	 * 
	 */

	//stuff to write to the doc
	int wordCountH;
	int wordCountV;

	// Use this for initialization
	void Start () {
		mode = 0;
		name = "";
		wordCountH = 0;
		txt = (TextAsset)Resources.Load("usa");
		dict = txt.text.Split("\n"[0]);
		count = 0;
		instructionsIndex = 0;
		input = "";
		lines = new string[19];
		displayed = new string[19];
		for (int i = 0; i < lines.Length; i++) {
			lines [i] = "";
			for (int j = 0; j < 100; j++) { 
				//just fill each line with 100 words at first :-P UNELEGANT, WHATEVER, I RATHER USE A WHILE LOOP
				int rand = Random.Range (0, dict.Length);
				lines[i] += dict [rand];
			}
			displayed [i] = lines [i].Substring (0, maxLineChara);
		}
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
			if (mode == 0) {
				name = input;
				input = "";
				display.text = "---";
				UIname.text = name;
				UIscore.text = "" + wordCountH;
				mode++;
				interval *= 0.25f;
			} else if (mode == 1) {
				if (display.text == instructions) {
					mode++;
					interval *= 4f;
				}
			} else if (mode == 2) {
				if (input.Length>2)
					checkMatch ();
			} else if (mode == 3) {
				if (display.text == instructions) {
					fillLinesV ();
					displayLinesV ();
					display.lineSpacing = 0.6f;
					mode++;
					interval *= 4f;
					count = 0;
//					totalTime = 15f;
				}
			} else if (mode == 4) {
				if (input.Length>2)
					checkMatchV ();
			} else if (mode == 5) {
			} else if (mode == 6) {
			}
		}


		if (mode==0) {
		} else {
			time += Time.deltaTime;
			if (time > interval) {
				time = 0;
				if (mode == 1) {
					if (instructionsIndex <= instructions.Length) {
						display.text = instructions.Substring (0, instructionsIndex);
						instructionsIndex++;
					}
				} else if (mode == 2) {
					count++;
					UItime.text = ""+(int)(totalTime-count*interval);
					if (count * interval > totalTime) {
						//end
						mode++;
						interval *= 0.25f;
						instructions = "great, thank you!\nlet's try it again, a little different this time.\n\nplease press enter to begin.";
						instructionsIndex = 0;
					}
					moveText ();
				} else if (mode==3) {
					if (instructionsIndex <= instructions.Length) {
						display.text = instructions.Substring (0, instructionsIndex);
						instructionsIndex++;
					}
				} else if (mode==4) {
					count++;
					UItime.text = ""+(int)(totalTime-count*interval);
					if (count * interval > totalTime) {
						//end
						mode++;
						interval *= 0.25f;
						display.lineSpacing = 1f;
						instructions = "processing..........";
						instructionsIndex = 0;
					}
					moveTextV ();
				} else if (mode==5) {
					if ((instructionsIndex + 8) <= instructions.Length) {
						display.text = instructions.Substring (0, instructionsIndex + 8);
						instructionsIndex++;
					} else {
						mode++;
						display.text = "some data stuff here";
					}
				} else if (mode==6){
				}
			}
		}

		//cheatz. highly buggy
		for (int k = 0; k < 7; k++) {
			if (Input.GetKeyDown (KeyCode.Alpha0+k)) {
				Debug.Log ("pressed "+k);
				if (k == 0) {
					SceneManager.LoadScene ("wordclimb");
				} else {
					mode = k;
				}
			}
		}
	}

	void checkMatch(){
		for (int i = 0; i < displayed.Length; i++) {
			if (displayed[i].IndexOf(input)>-1){
				string temp = displayed [i].Substring (0, displayed [i].IndexOf (input));
				for (int j = 0; j < input.Length; j++) {
					temp += " ";
				}
				temp += displayed [i].Substring (temp.Length, maxLineChara - temp.Length);
				displayed [i] = temp;
//				Debug.Log ("got it");
				wordCountH++;
				UIscore.text= ""+wordCountH;
				System.IO.File.AppendAllText ("worddatawords.txt", " "+input);
				displayText ();
				break;
			}
		}
		input = "";
		user.text = input;
	}

	//too lazy to make the above function able to handle both H and V
	void checkMatchV(){
		for (int i = 0; i < displayedV.Length; i++) {
			if (displayedV[i].IndexOf(input)>-1){
				string temp = displayedV [i].Substring (0, displayedV [i].IndexOf (input));
				for (int j = 0; j < input.Length; j++) {
					temp += " ";
				}
				temp += displayedV [i].Substring (temp.Length, 30 - temp.Length);
				displayedV [i] = temp;
				wordCountV++;
				UIscore.text= ""+wordCountV;
				System.IO.File.AppendAllText ("worddatawords.txt", " "+input);
				displayLinesV ();
				break;
			}
		}
		input = "";
		user.text = input;
	}

	void moveText(){
		//move stuff
		for (int i = 0; i < displayed.Length; i++) {
			displayed [i] = displayed[i].Substring(1)+lines[i][count+maxLineChara];
//			Debug.Log(displayed[i]);
		}
		displayText ();
	}

	void moveTextV(){
		for (int i = 0; i < displayedV.Length; i++) {
			displayedV [i] = displayedV [i].Substring (1) + linesV [i] [count + 30];
		}
		displayLinesV();
	}

	void displayText(){
		display.text = "";
		for (int i = 0; i < displayed.Length; i++) {
			display.text += displayed [i] + "\n";
		}
	}

	void fillLinesV(){
		for (int i = 0; i < linesV.Length; i++) {
			linesV [i] = "";
			for (int j = 0; j < 100; j++) { 
				//just fill each line with 100 words at first :-P UNELEGANT, WHATEVER, I RATHER USE A WHILE LOOP
				int rand = Random.Range (0, dict.Length);
				linesV[i] += dict [rand];
			}
			displayedV [i] = linesV [i].Substring (0, 30);//maxLineChara);
		}
	}

	void displayLinesV(){
		display.text = "";
		for (int i = 0; i < 30; i++) { //still using 19 lines
			for (int j = 0; j < displayedV.Length; j++) {
				display.text += displayedV [j] [i]+" ";
			}
			display.text += "\n";
		}
	}
}
