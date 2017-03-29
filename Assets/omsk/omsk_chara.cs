using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class omsk_chara : MonoBehaviour {

	/*
	 * you can run around and plant different stuff in the circle
	 * if you try to go out, the circle shrinks, less space to plant stuff (max plants allowed to be planted goes down)
	 * when the circle is very small, you can go out (one ending of the game)
	 * other ending of the game is when you like what you planted, you can press a button to stay
	 * --> that red bird comes up and "welcome to omsk" shows on screen
	 * 
	 * 
	 * how to plant: different keyboard keys (but not WASD)
	 * some combo of keys leads to stuff...like you have to press 3 diff ones to plant? (one on each row?)
	 * procedurally generated combos so that the replayability is good
	 * then i have to make a bunch of assets. so we'll see how that goes
	 * 
	 * 
	 * todo (?):
	 * -for when you are free and can go anywhere, fade it out as you go near the border and freeze player movement
	 * 	just so you don't go crazy out of bounds
	 */

	[SerializeField] GameObject limbs;
	[SerializeField] Animator animator;

	[SerializeField] omsk_manager manager;

	private float radius;

	private float speed=0.15f;
	private float stunTime = 0.25f;
	private float stunTimer;
	private bool isStunned;

	private bool isFree;

	// Use this for initialization
	void Start () {
		stunTimer = 0f;
		isStunned = false;
		isFree = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 increment = new Vector3 (Input.GetAxis ("Horizontal") * speed, 0f, Input.GetAxis ("Vertical") * speed);

		float dist = Vector3.Distance (Vector3.zero, transform.position + increment);

		Vector3 dir = increment;
		dir.Normalize ();
		dir *= -1;

		if (!isStunned) {
			if (dist < radius || isFree) {
				transform.position += increment;
			} else {
				//bump character back a bit
				isStunned = true;
				stunTimer = 0f;
				transform.position += dir;
				manager.bump ();
				animator.Play ("stun");
			}

			if (dir.magnitude != 0) {
				animator.Play ("walk");
				float rotationSpeed = 25f;
				limbs.transform.rotation = Quaternion.LookRotation (dir);
			} else {
				animator.Play ("idle");
			}

			if (Input.GetKeyDown (KeyCode.M)) {
				bool b = false;
				if (!isFree)
					b = manager.plant ();
				if (b) {
					//play animation for planting
				} else {
					//play animation for not being able to plant
				}
			}
		} else {
			stunTimer += Time.deltaTime;
			if (stunTimer > stunTime){
				isStunned = false;
				stunTimer = 0f;
			}
		}
	}

	public void setRadius(float r){
		radius = r;
	}

	public void setFree(bool b){
		isFree = b;
	}
}
