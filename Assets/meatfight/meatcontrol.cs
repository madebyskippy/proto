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

	/*
	 * if the punchtimer is on, then you can do the actual gameplay
	 * --take in input, fill in the "fill"
	 * --if the fill is full, send message to manager
	 * 
	 * else you can just flop around
	 * --take in input, do nothing (but diff action than if you are filling)
	 * 
	 */


	// Use this for initialization
	void Start () {
		mousedown = false;
		fill.text = "";
		managers = manager.GetComponent<mf_manager> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//mouse input
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		temp.z = 0f;
		temp.y -= 1.5f;
		transform.position = temp;

		if (Input.GetMouseButtonDown (0)) {
			mousedown = true;
			anim.Play ("meatjiggle");
		}
		if (Input.GetMouseButtonUp (0)) {
			mousedown = false;
			anim.Play ("meatstill");
		}
	}

	void wiggle(){
		Sequence sq = DOTween.Sequence();
		sq.Append(transform.DOScale(new Vector2(1.25f, 1.25f), 0.05f));
		sq.Append(transform.DOScale(new Vector2(1f, 1f), 0.05f));
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
