using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mf_manager : MonoBehaviour {
	[SerializeField] GameObject fighters;
	[SerializeField] GameObject[] hpbar;
	[SerializeField] GameObject hpbars;
	private Animator fighteranim;

	private float probability = 0.01f;

	private float punchtime = 1.5f; //3 second long punchtime
	private float timeLeft;
	private bool isPunching;

	private int fillLevel;
	private int fullLevel = 144;//1650; //1650 how many chara it takes to fill the screen at 40 pt font and 0.6 line spacing
	private bool isFull;

	private int[] hp;

	private string mode;
	private int cutNum;
	[SerializeField] GameObject button;

	// Use this for initialization
	void Start () {
		fighteranim = fighters.GetComponent<Animator> ();
		cutNum = 1;

		hp = new int[2];
		hp [0] = 100; hp [1] = 100;
		isPunching = false;

		isFull = false;
		fillLevel = 0;

		mode = "start";
		fighteranim.Play ("cut-intro-"+cutNum);

		button.SetActive (true);
		hpbars.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (mode == "start") {
//			if (Input.GetMouseButtonDown (0)) {
//				mode = "game";
//				fighteranim.Play ("fight-static");
//			}
		} else if (mode == "game") {
			if (Random.Range(0f,1f) < probability && !isPunching){
				Debug.Log ("puncho");
				//puncho!!!!
				//start punch timer, let player do the thing
				timeLeft = punchtime;
				isPunching = true;
				if (Random.Range(0f,1f)<0.5){ //left guy hit
					hp[0] -= 10;
					fighteranim.Play ("fight-punch-r");
				} else { //right guy hit
					hp[1] -= 10;
					fighteranim.Play ("fight-punch-l");
				}
				updatehp ();
			}

			if (isPunching) {
				timeLeft -= Time.deltaTime;
			}
			if (timeLeft < 0) {
				isPunching = false;
				fighteranim.Play ("fight-static");
			}

			if (isFull) {
				//screen is full, win!!!
				Debug.Log("win. eat.");
				mode = "end";
				isPunching = false;
				fighteranim.Play ("cut-end-win");
			}
			if ((hp [0] <= 0 || hp [1] <= 0) && !isPunching) {
				//lose!!!
				mode = "end";
				//				isPunching = false;
				if (hp [0] <= 0) {
					fighteranim.Play ("cut-end-lose-l");
				} if (hp [1] <= 0) {
					fighteranim.Play ("cut-end-lose-r");
				}
			}
		} else if (mode == "end") {
		}

		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			SceneManager.LoadScene ("meatfight");
		}

	}

	void updatehp(){
		hpbar [0].transform.localScale = new Vector3 (hp [0] / 100f * 5, 0.5f, 1f);
		hpbar [1].transform.localScale = new Vector3 (hp [1] / 100f * 5, 0.5f, 1f);
	}

	public void setFill(int l){
		fillLevel = fillLevel + l;
		if (fillLevel >= fullLevel) {
			isFull = true;
		}
	}

	public bool getIsPunching(){
		return isPunching;
	}

	public void increaseCutNum(){
		if (cutNum < 6) {
			cutNum++;
			fighteranim.Play ("cut-intro-" + cutNum);
		} else {
			mode = "game";
			fighteranim.Play ("fight-static");
			button.SetActive (false);
			hpbars.SetActive (true);
		}
	}

	public string getMode(){
		return mode;
	}
}
