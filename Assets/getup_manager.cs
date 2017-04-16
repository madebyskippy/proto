using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class getup_manager : MonoBehaviour {

	[SerializeField] GameObject instruc;
	[SerializeField] GameObject winscreen;
	[SerializeField] GameObject frame;

	private GameObject[] frames;
	private int[] order;
	private List<int> temp_order;

	private List<int> shown;
	private List<int> pressed;

	private bool isDead;

	// Use this for initialization
	void Start () {
		winscreen.SetActive (false);

		frames = new GameObject[frame.transform.childCount];
		pressed = new List<int>();

		for (int i = 0; i < frames.Length; i++) {
			frames [i] = frame.transform.GetChild (i).gameObject;
			frames [i].SetActive (false);
		}
		order = new int[frames.Length];
		temp_order = new List<int> ();
		for (int i = 0; i < order.Length; i++) {
			temp_order.Add(i);
		}
		for (int i = 0; i < order.Length; i++) {
			int temp = temp_order [Random.Range (0, temp_order.Count)];
			order [i] = temp;
			temp_order.Remove (temp);
		}
		isDead = true;

		shown = new List<int>();
		for (int i = 0; i < 3; i++) {
			shown.Add((int)(i*frames.Length/3)+Random.Range(0,frames.Length/3));
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (!instruc.activeSelf) {
				SceneManager.LoadScene ("getup");
			} else {
				instruc.SetActive (false);
				isDead = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (!isDead) {
			for (int i = 0; i < 26; i++) {
				if (Input.GetKeyDown (KeyCode.A + i)) {
					press(i);
				}
			}
			for (int i = 0; i < 10; i++) {
				if (Input.GetKeyDown (KeyCode.Alpha1 + i)) {
					press(26+i);
				}
			}
			if (Input.GetKeyDown (KeyCode.Alpha0)) {
				press (35);
			}
				
			for (int i = 0; i < frames.Length; i++) {
				if (shown.Contains (i)) {
					frames [i].SetActive (true);
				} else {
					frames [i].SetActive (false);
				}
			}

			if (pressed.Count == frames.Length) {
				bool win = true;
				for (int i = 0; i < frames.Length; i++) {
					if (pressed [i] != i) {
						win = false;
						break;
					}
				}
				if (win) {
					winscreen.SetActive (true);
//					Debug.Log ("u win");
					for (int i = 0; i < frames.Length; i++) {
						frames [i].SetActive (false);
					}
					frames [frames.Length - 1].SetActive (true);
				}
			}

			if (pressed.Count > frames.Length) {
				pressed.Remove (0);
			}

		}
	}

	void press(int c){
		if (shown[shown.Count-1] != c){
			shown.RemoveAt (0);
			shown.Add (order [c]);
			pressed.Add (c);
		}
	}

	void dead(){
		Debug.Log ("dead");
	}

	void end(){
		Debug.Log ("end");
	}
}
