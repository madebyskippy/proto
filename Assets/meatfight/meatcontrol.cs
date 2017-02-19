using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class meatcontrol : MonoBehaviour {
	[SerializeField] Text fill;

	[SerializeField] GameObject manager;
	private mf_manager managers;

	Animator anim;

	private bool mousedown;

	private float lastX;

	// Use this for initialization
	void Start () {
		mousedown = false;
		fill.text = "";
		managers = manager.GetComponent<mf_manager> ();
		anim = GetComponent<Animator> ();
//		anim.Play ("meatjiggle");
		lastX = transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		//mouse input
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		temp.z = 0f;
		temp.y -= 1.5f;
		transform.position = temp;



		lastX = transform.position.x;

		if (Input.GetMouseButtonDown (0)) {
			mousedown = true;
			if (managers.getIsPunching ()) {
				anim.Play ("meatjuice-1");
				//				anim.PlayQueued ("meatjuice-2");
			} else {
				anim.Play ("meatjiggle");
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			mousedown = false;
			if (managers.getIsPunching()) {
				anim.Play ("meatjuice-3");
			} else {
				anim.Play ("meatstill");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "grid") {
			if (managers.getIsPunching() && mousedown) {
				col.gameObject.tag = "gridFull";
				managers.setFill (1);
				Color temp = col.gameObject.GetComponent<SpriteRenderer> ().color;
				temp.a = 1f;
				col.gameObject.GetComponent<SpriteRenderer> ().color = temp;
			}
		}
	}
}
