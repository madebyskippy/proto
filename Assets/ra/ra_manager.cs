using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ra_manager : MonoBehaviour {

	[SerializeField] Text text;

	private string[] tweets;
	private List<string> tweetsToRandomize;
	private bool[] highlighted;
	private TextAsset textfile;

	// Use this for initialization
	void Start () {
		textfile = (TextAsset)Resources.Load("tweets");
		tweets = textfile.text.Split("\n"[0]);
		tweetsToRandomize = new List<string> ();
		tweetsToRandomize.AddRange (tweets);
		highlighted = new bool[tweets.Length];
		text.text = "";
//		for (int i = 0; i < tweets.Length; i++) {
////			text.text += tweets [i] + "   ";
//			highlighted[i] = false;
//			int index = i;//Random.Range(0, tweetsToRandomize.Count);
////			tweets [i] = tweetsToRandomize [index];
////			text.text += tweetsToRandomize [index] + "   ";
////			for (int j=0; j<tweets[index].Length; j++){
////				text.text += tweets [index] [j];//" ";
////			}
//			text.text += tweets[i];
//			//			tweetsToRandomize.RemoveAt (index);
//			text.text += "\n";
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene ("ra");
		}
	}

	void displayText(){
		text.text = "";
		for (int i = 0; i < tweets.Length; i++) {
			if (highlighted [i]) {
				text.text += tweets [i];
			} else {
				for (int j=0; j<tweets[i].Length; j++){
					text.text += " ";
				}
			}
			text.text += "\n";
		}
	}

	void randomizeText(){
		text.text = "";
		tweetsToRandomize.Clear();
		tweetsToRandomize.AddRange (tweets);
		for (int i = 0; i < tweets.Length; i++) {
			int index = Random.Range(0, tweetsToRandomize.Count);
			text.text += tweetsToRandomize [index] + "   ";
			tweetsToRandomize.RemoveAt (index);
		}
	}

	void clearHighlighted(){
		for (int i = 0; i < highlighted.Length; i++) {
			highlighted [i] = false;
		}
	}

	public void makeText(){
		highlighted [Random.Range (0, highlighted.Length)] = true;
		displayText ();
	}

}
