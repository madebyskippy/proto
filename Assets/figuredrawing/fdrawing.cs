using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class fdrawing : MonoBehaviour {

	/*
	 * grid is 100x55
	 * change field of view from 60 --> 110
	 */

	private float totaltime = 3 * 60f;
	private float currenttime;

	[SerializeField] AudioSource whitenoise;
	[SerializeField] AudioSource beep;
	[SerializeField] AudioSource clock;
	[SerializeField] AudioSource[] papers;
	private float soundprobability = 0.01f;

	[SerializeField] Camera cam;
	[SerializeField] GameObject start;
	[SerializeField] GameObject end;
	[SerializeField] GameObject gridscript;
	[SerializeField] GameObject hand;
	[SerializeField] GameObject otherhand;
	[SerializeField] GameObject[] allthestuff;
	[SerializeField] GameObject fade;
	private List<GameObject> grids;
	private bool mousedown;

	private bool isend;

	// Use this for initialization
	void Start () {
		mousedown = false;
		grids = gridscript.GetComponent<CS_GridSetup> ().getgrid ();
		whitenoise.volume = 0;
		currenttime = 0;
		isend = false;
		start.SetActive (true);
		end.SetActive (false);
		Invoke ("turnOffStart", 12f); //after 12 seconds + 3 sec fade
	}

	// Update is called once per frame
	void Update () {
		currenttime += Time.deltaTime;
		if (currenttime > totaltime && !isend) {
			//game end
			Debug.Log("end");
			isend = true;
			endgame ();
		}
		if (!isend) {
			//linear equations for now
			cam.fieldOfView = 60f + 50f * Mathf.Pow(currenttime / totaltime, 2);
			whitenoise.volume = (0.1f) * Mathf.Pow(currenttime / totaltime, 2);

			if (Random.Range (0f, 1f) < soundprobability) {
				papers [(int)Random.Range (0f, papers.Length)].Play ();
			}

			if (Input.GetMouseButtonDown (0)) {
				mousedown = true;
			}
			if (Input.GetMouseButtonUp (0)) {
				mousedown = false;
			}


			if (mousedown) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.tag == "Player") {
						hit.collider.gameObject.GetComponent<SpriteRenderer> ().color = Color.black;
						Vector3 pos = hit.collider.gameObject.transform.position;
						pos.x += 5.85f;
						pos.y -= 9.65f;

						hand.transform.position = pos;
					}
				}
			}
		}
	}

	void turnOffStart(){
//		start.SetActive (false);
		SpriteRenderer sr = start.GetComponent<SpriteRenderer>();
		DOTween.ToAlpha (() => sr.color, x => sr.color = x, 0f,1f);
	}

	void endgame(){
		beep.Play ();
		clock.Stop ();
		end.SetActive (true);
		SpriteRenderer sr = end.GetComponent<SpriteRenderer> ();
		DOTween.ToAlpha (() => sr.color, x => sr.color = x, 1f,3f);
		SpriteRenderer srf = fade.GetComponent<SpriteRenderer> ();
		DOTween.ToAlpha (() => srf.color, x => srf.color = x, 1f,3f);
		Invoke ("turnoff", 3f);

	}

	void turnoff(){
		//		cam.DOFieldOfView (80f, 10f);
		string temp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
//		Debug.Log (temp);
//		Application.CaptureScreenshot("../Screenshot"+temp+".png");
		for (int i = 0; i < allthestuff.Length; i++) {
			allthestuff [i].SetActive (false);
		}
	}
}
