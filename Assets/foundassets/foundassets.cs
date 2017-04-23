using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class foundassets : MonoBehaviour {

	[SerializeField] AudioSource roll;
	[SerializeField] AudioSource jingle;
	[SerializeField] AudioSource wrong;

	[SerializeField] Text s;
	[SerializeField] GameObject e;
	[SerializeField] Sprite[] circles;
	[SerializeField] GameObject prefab;
	float topX = -3.1f; //-2.31
	float topY = 2.25f; //1.5
	float width = 0.775f;
	float height = -0.75f;

	private List<GameObject>[] col; //9 cols of 14

	bool[] fingerdown;
	bool insequence;
	int lastkey;

	int target;
	int score;
	float interval = 5f;
	float timer;
	int lives = 4;

	//{16,22,4,17,19,24,20,8,14,15}
	private int[] keys = {0,18,3,5,6,7,9,10,11};

	// Use this for initialization
	void Start () {
		fingerdown = new bool[]{false,false,false,false,false,false,false,false,false};
		insequence = false;
		lastkey = -1;
		e.SetActive (false);

		roll.volume = 0f;
		wrong.volume = 0f;

		col = new List<GameObject>[9];
		for (int i = 0; i < col.Length; i++) {
			col [i] = new List<GameObject> ();
			for (int j = 0; j < 7; j++) {
//				Debug.Log (i+", "+j+", "+col [i,j]);
				GameObject temp = Instantiate (prefab, new Vector3(topX+width*i,topY+height*j,0f), Quaternion.identity) as GameObject;
				col[i].Add(temp);
				col [i][j].GetComponent<SpriteRenderer> ().sprite = circles [Random.Range (0, circles.Length)];
			}
		}

		newtarget ();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene ("foundassets");
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		for (int i = 0; i < circles.Length; i++) {
			if (Input.GetKeyDown(KeyCode.A+keys[i])){
				fingerdown[i] = true;


				if (insequence) {
					if (i + 1 == lastkey || i - 1 == lastkey) {
						insequence = true;
						lastkey = i;
					} else {
						insequence = false;
					}
				} else {
					if (i == lastkey || lastkey == -1) {
						insequence = true;
						lastkey = i;
					}
				}

			}
			if (Input.GetKeyUp (KeyCode.A + keys [i])) {
				fingerdown[i] = false;
			}
		}

		if (checkdown() && insequence) {
			//			GetComponent<SpriteRenderer> ().color = new Color (1,1,1,1);
			roll.volume = 1f;
			wrong.volume = 0f;
			e.SetActive(false);
		} else {
			//			GetComponent<SpriteRenderer> ().color = new Color (1,1,1,0.8f);
			roll.volume = 0f;
			wrong.volume = 1f;
			e.SetActive(true);
		}


		if (lastkey > -1) {
			timer += Time.deltaTime;
			rot (lastkey);
			if (lastkey == target) {
				jingle.Play ();
				score++;
				targetpop (target);
				newtarget ();
				Debug.Log ("score: " + score);
				interval *= 0.95f;
			} else if (timer > interval) {
				decrease ();
				targetflash (target,Color.red);
				newtarget ();
			}
		}

		if (lives < 1) {
			lastkey = -2;
			s.text = "" + score;
		} else {
			s.text = "";
		}

	}

	void targetpop(int t){
		for (int j = 0; j < col[t].Count; j++) {
			col [t] [j].GetComponent<SpriteRenderer> ().color = Color.white;
			col [t][j].transform.localScale = Vector3.one * 1.3f;
			col [t] [j].transform.DOScale (Vector3.one, 0.5f);
		}
	}

	void targetflash(int t, Color c){
		for (int j = 0; j < col[t].Count; j++) {
			col [t][j].GetComponent<SpriteRenderer>().color=c;
			col [t] [j].GetComponent<SpriteRenderer> ().DOColor (Color.white,0.75f);
		}
	}

	void newtarget(){
		int old = target;
		target = Random.Range (0, 9);
//		for (int j = 0; j < col[old].Count; j++) {
//			col [old][j].GetComponent<SpriteRenderer>().color=Color.white;
//		}
		for (int j = 0; j < col[target].Count; j++) {
			col [target] [j].GetComponent<SpriteRenderer>().color=new Color(0.5f,0.75f,1f,1f);
		}
		timer = 0;
	}

	void decrease(){
		lives--;
		for (int i = 0; i < col.Length; i++) {
			for (int j = 0; j < col [0].Count; j++) {
				if (j < (4 - lives) || j > (col[0].Count - 1 - (4-lives))) {
					col [i] [j].SetActive (false);
				}
			}
		}
	}

	void rot(int c){
		for (int j = 0; j < col[c].Count; j++) {
			col [c][j].transform.Rotate(0f,0f,2f);
		}
	}

	void move(int c){
		for (int j = 0; j < col[c].Count; j++) {
			col [c][j].transform.position = new Vector3(topX+width*c,topY+height*j,0f);
		}
	}

	bool checkdown(){
		bool t = false;
		for (int i = 0; i < fingerdown.Length; i++) {
			if (fingerdown [i]) {
				t = true;
			}
		}
		return t;
	}
}
