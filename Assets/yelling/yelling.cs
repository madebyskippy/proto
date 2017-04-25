using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yelling : MonoBehaviour {

	/*
	 * todo: trackpad drawings and then generate them shaky and random (jiggle the scale and position a little)
	 * 
	 * maybe just put a diff pattern on each circle so it shows something interesting
	 * like a diff gobbldygook of yelling people or something
	 */

	[SerializeField] Animator small;
	[SerializeField] Mesh test;
	[SerializeField] GameObject rings;
	[SerializeField] lines[] lr;

	float pump_timer;
	float tire_timer;

	float maxyell = 3.5f;
	float power;

	bool pumping;
	bool resting;
	bool end;

	// Use this for initialization
	void Start () {
//		lr = linez.transform.GetComponentsInChildren<lines> ();
		pump_timer = 0;
		pumping = false;
		resting = false;
		end = false;
//		lr = new lines[10];
		for (int i = 0; i < lr.Length; i++){
//			GameObject l = Instantiate (linez);
//			l.SetActive (true);
//			lr [i] = l.GetComponent<lines>();
			lr [i].Init ();
			lr [i].setV(test.vertices);
			lr [i].scale (Random.Range(0.45f,2.5f));
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("yelling");
		}if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!resting && !end) {
				small.Play ("pumpup");
				pump_timer = 0;
				pumping = true;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (pumping && !end) {
				pumping = false;
				power = Mathf.Clamp (pump_timer, 0.1f, maxyell);
				yell ();
			}
		}

		if (pumping) {
			pump_timer += Time.deltaTime;
		}

		if (small.gameObject.transform.localScale.x <= 0.2f) {
			//endgame
			end = true;
		}

		if (!pumping && !resting) {
			for (int i = 0; i < lr.Length; i++) {
				lr [i].jiggle ();
			}
		}
		if (pumping) {
			for (int i = 0; i < lr.Length; i++) {
				lr [i].warp ();
			}
		}
		if (resting) {
			for (int i = 0; i < lr.Length; i++) {
				lr [i].pop ();
			}
		}
	}

	void yell(){
		small.gameObject.transform.localScale *= 0.9f;
//		Debug.Log (pump_timer);
		small.Play ("yell");
		Invoke ("tire", power);
		rings.GetComponent<rings> ().changeSize (5f+power*2f);
		rings.GetComponent<rings> ().changePause (0.3f + Mathf.Clamp(1f/power,0f,.5f));
		rings.GetComponent<rings> ().begin ();
		resting = true;

	}

	void tire(){
		for (int i = 0; i < lr.Length; i++) {
			lr [i].settarget ();
		}
		resting = false;
		small.Play ("idle");
	}

}
