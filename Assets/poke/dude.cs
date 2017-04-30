using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dude : MonoBehaviour {

	[SerializeField] SpriteRenderer head;
	[SerializeField] SpriteRenderer hat;
	[SerializeField] SpriteRenderer bod;
	[SerializeField] SpriteRenderer leggy;

	Animator anim;
	GameObject manager;
	int r; int c;

	bool isPoked = false;

	// Use this for initialization
	void Start () {
		head.color = 	Random.ColorHSV(0f, 1f, 0f, 0.15f, 0.85f, 1f);
		hat.color = 	Random.ColorHSV(0f, 1f, 0f, 0.15f, 0.85f, 1f);
		bod.color = 	Random.ColorHSV(0f, 1f, 0f, 0.15f, 0.85f, 1f);
		leggy.color = 	Random.ColorHSV(0f, 1f, 0f, 0.15f, 0.85f, 1f);

		anim = GetComponent<Animator> ();
		manager = GameObject.FindGameObjectWithTag ("Player");
	}

	public void Init(int x, int y){
		Start ();
		r = x;
		c = y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		manager.GetComponent<poke> ().click (r,c);
		isPoked = true;
	}

	public bool getIsPoked(){
		return isPoked;
	}

	public void reset(){
		anim.Play ("straight");
		isPoked = false;
	}
}
