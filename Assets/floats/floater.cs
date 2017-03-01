using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class floater : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	private float magnitude = 0.5f;//100f;
	private float time;
	private float size;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

		time = Random.Range (5f, 15f);
		size = Random.Range (0.5f, 1.5f);

		Color temp = sr.color;
		temp.a = 0f;
		sr.color = temp;
		transform.localScale = Vector3.one * size / 2f;
		cycle ();

		InvokeRepeating ("changeVelocity", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
//		rb.rotation = Vector3.Angle (Vector3.up, rb.velocity) * Vector3.Cross (Vector3.up, rb.velocity).normalized.z;
	}

	void changeVelocity(){
		rb.velocity += new Vector2 (Random.Range(-1f,1f) * magnitude, Random.Range(-1f,1f) * magnitude) ;
	}

	void addForce(){
		Vector2 direction = new Vector2 (Random.Range(-1f,1f),Random.Range(-1,1f));
		rb.AddForce (direction * magnitude);
	}

	void cycle(){
		Sequence sqA = DOTween.Sequence ();
		sqA.Append (DOTween.ToAlpha (() => sr.color, x => sr.color = x, 0.75f, time/2f));
		sqA.Append (DOTween.ToAlpha (() => sr.color, x => sr.color = x, 0f, time/2f));
//		DOTween.ToAlpha (() => sr.color, x => sr.color = x, 0.25f, time);
		Sequence sqS = DOTween.Sequence ();
		sqS.Append (transform.DOScale (new Vector2 (1f, 1f) * size, time / 2f));
		sqS.Append (transform.DOScale (new Vector2 (1f, 1f) * size / 2f, time / 2f));
		Invoke ("cycle", time);
	}
}
