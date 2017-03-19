using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tablemanager : MonoBehaviour {

	[SerializeField] Camera cam;
	[SerializeField] Text ui;

	private bool started;

	// Use this for initialization
	void Start () {
		ui.text = "it's too bright to party!!! knock the light off!\n\nclick the objects to spawn shapes";
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			SceneManager.LoadScene ("table");
		}
		if (!started && Input.GetMouseButtonDown (0)) {
			started = true;
			ui.text = "";
		}
	}

	public void dead(){
		int objnum = GameObject.FindGameObjectsWithTag ("Respawn").Length;

		ui.text = "yeeea now it's time to party. you used "+objnum+" objects!!!";
	}
}
