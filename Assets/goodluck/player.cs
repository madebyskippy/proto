using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public GameObject manager;
	int speed = 5;

	private float stunTime = 1f;
	private float timeLeft;
	private bool isStunned;

	// Use this for initialization
	void Start () {
		timeLeft = stunTime;
		isStunned = false;
	}
	
	// Update is called once per frame
	void Update () {


		if (isStunned) {
			timeLeft -= Time.deltaTime;
		} else {
			float deltaX = Input.GetAxis ("Horizontal") * Time.deltaTime;
			float newX = transform.position.x + deltaX * speed;

			if (deltaX == 0) {
				GetComponent<Animator> ().Play ("chara_idle");
			}else if (deltaX > 0) {
				GetComponent<Animator> ().Play ("chara_walk");
			} else {
				GetComponent<Animator> ().Play ("chara_walk_L");
			}

			if (newX < 3.9f && newX > -3.9f) {
				transform.position += new Vector3 (deltaX * speed, 0f, 0f);
			}
		}


		if (timeLeft < 0) {
			//done being stunned
			isStunned=false;
			timeLeft = stunTime;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("something hit player");
		manager.GetComponent<manager> ().decreaseLife();
		Destroy (collider.gameObject);
		isStunned = true;
		stun ();
	}

	void stun(){
		GetComponent<Animator> ().Play ("chara_ouch");
	}
}
