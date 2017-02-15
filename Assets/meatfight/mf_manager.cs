using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mf_manager : MonoBehaviour {
	[SerializeField] GameObject fighters;
	[SerializeField] GameObject[] hpbar;
	private Animator fighteranim;

	private float probability = 0.01f;

	private float punchtime = 3f; //3 second long punchtime
	private float timeLeft;
	private bool isPunching;

	private int fillLevel;
	private int fullLevel = 1650; //1650 how many chara it takes to fill the screen at 40 pt font and 0.6 line spacing
	private bool isFull;

	private int[] hp;

	// Use this for initialization
	void Start () {
		fighteranim = fighters.GetComponent<Animator> ();

		hp = new int[2];
		hp [0] = 100; hp [1] = 100;
		isPunching = false;

		isFull = false;
		fillLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0f,1f) < probability && !isPunching){
			Debug.Log ("puncho");
			//puncho!!!!
			//start punch timer, let player do the thing
			timeLeft = punchtime;
			isPunching = true;
			if (true) { //Random.Range(0f,1f)<0.5){ //left guy hit
				//play the sprite stuff
				hp[0] -= 10;
				fighteranim.Play ("punchr");
			} else { //right guy hit
				//i need sprites for this so it's not in yet --TODO
			}
			updatehp ();
		}

		if (isPunching) {
			timeLeft -= Time.deltaTime;
		}
		if (timeLeft < 0) {
			isPunching = false;
			fighteranim.Play ("static");
		}

		if (isFull) {
			//screen is full, win!!!
			Debug.Log("win. eat.");
		}
		if (hp [0] < 0 || hp [1] < 0) {
			//lose!!!
			Debug.Log("someone fuckin died!!!");
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
}
