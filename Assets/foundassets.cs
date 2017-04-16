using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foundassets : MonoBehaviour {

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

	private int[] keys = {16,22,4,17,19,24,20,8,14,15};

	// Use this for initialization
	void Start () {
		fingerdown = new bool[]{false,false,false,false,false,false,false,false,false};
		insequence = false;
		lastkey = -1;

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
		for (int i = 0; i < circles.Length; i++) {
			if (Input.GetKeyDown(KeyCode.A+keys[i])){
				GameObject temp = col[i][0];
				col [i].RemoveAt (0);
				col [i].Add (temp);
//				move (i);
				fingerdown[i] = true;

				if (i + 1 == lastkey || i - 1 == lastkey) {
					insequence = true;
					lastkey = i;
				} else {
					insequence = false;
				}

			}
			if (Input.GetKeyUp (KeyCode.A + keys [i])) {
				fingerdown[i] = false;
			}
		}

		if (checkdown()) {
			GetComponent<SpriteRenderer> ().color = new Color (1,1,1,1);
		} else {
			GetComponent<SpriteRenderer> ().color = new Color (1,1,1,0.8f);
		}

		if (insequence) {
			transform.localScale = Vector3.one;
		} else {
			transform.localScale = Vector3.one * 0.8f;
		}

		if (lastkey != -1) {
			rot (lastkey);
			if (lastkey == target) {
				score++;
				newtarget ();
				Debug.Log ("score: " + score);
			}
		}
	}

	void newtarget(){
		target = Random.Range (0, 9);
		for (int j = 0; j < col[target].Count; j++) {
			col [target][j].GetComponent<SpriteRenderer>().color=Color.red;
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
