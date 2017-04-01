using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
	 * todo:
	 * 	-key mapping so it's a keyboard game :-)
	 * 	-animation feedback
	 */
	[SerializeField] Image[] plantIndicators;

	[SerializeField] GameObject limbs;
	[SerializeField] Animator animator;

	[SerializeField] omsk_manager manager;

	private float radius;

	private float speed=0.15f;
	private float stunTime = 0.25f;
	private float stunTimer;
	private bool isStunned;

	private bool isFree;

	private int[] top = {16,4,17,19,24,20,8,14,15};
	private int[] mid = { 5, 6, 7, 9, 10, 11 };
	private int[] btm = { 25, 23, 2, 21, 1, 13, 12 };

	private int[] plant = {-1,-1,-1};

	// Use this for initialization
	void Start () {
		stunTimer = 0f;
		isStunned = false;
		isFree = false;

		plantIndicators [0].color = new Color (.75f, .75f, .75f);
		plantIndicators [1].color = new Color (.75f, .75f, .75f);
		plantIndicators [2].color = new Color (.75f, .75f, .75f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 increment = new Vector3 (Input.GetAxis ("Horizontal") * speed, 0f, Input.GetAxis ("Vertical") * speed);

		float dist = Vector3.Distance (Vector3.zero, transform.position + increment);

		Vector3 dir = increment;
		dir.Normalize ();
		dir *= -1;

		if (!isStunned) {
			if (isFree) {
				Vector3 temp = transform.position + increment;
				temp.z = Mathf.Clamp (temp.z, -6.75f, 20f);
				float xlimit = temp.z * (26f / 26.75f) + 9.57f;
//				Debug.Log (xlimit);
				temp.x = Mathf.Clamp (temp.x, xlimit * -1, xlimit);
				transform.position = temp;
			} else {
				if (dist < radius) {
					transform.position += increment;
				} else {
					//bump character back a bit
					isStunned = true;
					stunTimer = 0f;
					transform.position -= new Vector3 (transform.position.x, 0f, transform.position.z).normalized;
					manager.bump ();
					animator.Play ("stun");
				}
			}

			if (dir.magnitude != 0) {
				animator.Play ("walk");
				float rotationSpeed = 25f;
				limbs.transform.rotation = Quaternion.LookRotation (dir);
			} else {
				animator.Play ("idle");
			}

			for (int i = 0; i < 26; i++) {
				if (Input.GetKeyDown (KeyCode.A + i)) {
					if (i != 0 && i != 3 && i!=18 && i!=22) {
						if (Array.IndexOf (top, i) > -1) {
							plant [0] = Array.IndexOf (top, i);
							plantIndicators [0].color = new Color (1f, .75f, .75f);
						}
						if (Array.IndexOf (mid, i) > -1) {
							plant [1] = Array.IndexOf (mid, i);
							plantIndicators [1].color = new Color (1f, .75f, .75f);
						}
						if (Array.IndexOf (btm, i) > -1) {
							plant [2] = Array.IndexOf (btm, i);
							plantIndicators [2].color = new Color (1f, .75f, .75f);
						}
						Debug.Log (plant[0]+","+plant[1]+","+plant[2]);
					}
				}
			}

			if (/*Input.GetKeyDown (KeyCode.Space) && */(plant[0]!=-1 && plant[1]!=-1 && plant[2]!=-1)) {
				bool b = false;
				if (!isFree)
					b = manager.plant (plant);
				if (b) {
					//play animation for planting
					animator.Play ("plant");
				} else {
					//play animation for not being able to plant
					animator.Play ("noneleft");
				}
				plant [0] = -1; plant [1] = -1; plant [2] = -1;
				plantIndicators [0].color = new Color (.75f, .75f, .75f);
				plantIndicators [1].color = new Color (.75f, .75f, .75f);
				plantIndicators [2].color = new Color (.75f, .75f, .75f);
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
