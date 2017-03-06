using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fdrawing : MonoBehaviour {

	/*
	 * grid is 100x55
	 * change field of view from 60 --> 110
	 */

	private float totaltime = 10f;//3 * 60f;
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
	[SerializeField] GameObject[] allthestuff;
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
		Invoke ("turnOffStart", 15f); //after 15 seconds
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
			cam.fieldOfView = 60f + 50f * (currenttime / totaltime);
			whitenoise.volume = (0.1f) * (currenttime / totaltime);

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
		start.SetActive (false);
	}

	void endgame(){
		beep.Play ();
		clock.Stop ();
		end.SetActive (true);
		cam.fieldOfView = 60f;
		for (int i = 0; i < allthestuff.Length; i++) {
			allthestuff [i].SetActive (false);
		}
	}
}
