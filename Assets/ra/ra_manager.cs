using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ra_manager : MonoBehaviour {

	[SerializeField] GameObject mask;
	[SerializeField] GameObject[] kids;
	[SerializeField] Text text;
	[SerializeField] GameObject fade;

	[SerializeField] AudioSource audio;

	[SerializeField] ra_mouse mouse;

	private List<GameObject> masks;
	private int total = 400; //to lose game

	private TextAsset textfile;
	private string[] tweets;

	private bool inGame;

	// Use this for initialization
	void Start () {
		fade.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
		inGame = false;
		masks = new List<GameObject> ();
		textfile = (TextAsset)Resources.Load("tweets");
		tweets = textfile.text.Split("\n"[0]);
		text.text = "";
		audio.volume = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene ("ra");
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (Input.GetMouseButtonDown (0)) {
			if (!inGame && masks.Count < total) {
				inGame = true;
				//start game
				mouse.setMode(true);
				GameObject currentKid = kids [Random.Range (0, kids.Length)];
				GameObject[] fillings = currentKid.GetComponent<ra_kid> ().getGrowths ();
				for (int i = 0; i < fillings.Length; i++) {
					fillings [i].GetComponent<ra_growth> ().poof ();
				}
			} else {
				//restart
			}
		}
		audio.volume = masks.Count / (total*1f);
		if (masks.Count > total) {
			//end game
			if (inGame) {
				inGame = false;
				mouse.setMode(false);
				//show the tweet
//				fade.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
//				fade.GetComponent<SpriteRenderer>().doa
				SpriteRenderer sr = fade.GetComponent<SpriteRenderer>();
				DOTween.ToAlpha (() => sr.color, x => sr.color = x, 1f, 2f);
				text.text = tweets[Random.Range(0,tweets.Length)];;
			}
		}

		for (int i = 0; i < masks.Count; i++) {
			//jitter them
			Vector3 incr = new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.05f, 0.05f), 0f);
			incr.x = Mathf.Clamp (incr.x, -8f, 8f);
			incr.y = Mathf.Clamp (incr.y, -4.5f, 4.5f);
			masks [i].transform.position += incr;
		}
	}

	public void addMask(){
		GameObject temp = Instantiate (mask);
		Vector3 pos = new Vector3 (Random.Range(-8f,8f), Random.Range(-4.5f,4.5f), 0f);
		temp.transform.position = pos;

		Vector3 scale = temp.transform.localScale;
		scale.x = Random.Range (3f, 10f);
		scale.y = Random.Range (0.075f, 0.2f);
//		temp.transform.localScale = scale;
		temp.transform.localScale=Vector3.zero;
		temp.transform.DOScale (scale, 0.25f);//.SetEase(Ease.InCirc);

		masks.Add (temp);
	}
}
