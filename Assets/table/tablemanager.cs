using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tablemanager : MonoBehaviour {

	[SerializeField] Camera cam;
	[SerializeField] Text ui;
	[SerializeField] GameObject fade;

	private bool started;
	public bool ended;

	private int count;

	// Use this for initialization
	void Start () {
		ui.text = "it's too bright to party!!! knock the light off!\n\nclick the objects to spawn shapes";
		ui.text += "\nthe shapes will change size and disappear,\nclick to set them as they are";
		started = false;
		ended = false;
		count = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene ("table");
		}
		if (!started && Input.GetMouseButtonDown (0)) {
			count--;
			if (count < 0) {
				started = true;
				ui.text = "";
			}
		}
	}

	public void dead(){
		if (!ended) {
			ended = true;		

			fade.GetComponent<SpriteRenderer> ().sortingOrder = 0;
			Color temp = fade.GetComponent<SpriteRenderer> ().color;
			temp.a = 0.5f;
			fade.GetComponent<SpriteRenderer> ().color = temp;

			GameObject[] objs = GameObject.FindGameObjectsWithTag ("Respawn");
			int objnum = objs.Length;

			for (int i = 0; i < objnum; i++) {
				objs [i].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
				objs [i].GetComponent<tableshape> ().freeze ();
				objs [i].GetComponent<SpriteRenderer> ().color = new Color (221f / 255f, 148f / 255f, 72f / 255f);
				if (Random.Range (0f, 1f) < 0.2f) {
					objs [i].GetComponent<SpriteRenderer> ().color = new Color (33f / 255f, 44f / 255f, 57f / 255f);
				}
			}

			ui.text = "yeeea now it's time to party!!!!!!\n\nyou used " + (objnum) + " objects!!!\nspace to restart";
		}
	}
}
