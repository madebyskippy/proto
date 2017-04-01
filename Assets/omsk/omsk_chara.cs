using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class omsk_chara : MonoBehaviour {

	/*
	 * todo:
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

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		changeMode (-1);
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
//				animator.Play ("walk");
				if (animator.GetInteger ("mode") == -1) {
					changeMode (0);
				}
				float rotationSpeed = 25f;
				limbs.transform.rotation = Quaternion.LookRotation (dir);
			} else {
//				animator.Play ("idle");
				if (animator.GetInteger ("mode") == 0) {
					changeMode (-1);
				}
			}

			if (Input.GetKeyDown (KeyCode.Space) ) {
				bool b = false;
				if (!isFree)
					b = manager.plant ();
				if (b) {
					//play animation for planting
					animator.Play ("plant");
				} else {
					//play animation for not being able to plant
					animator.Play ("noneleft");
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

	//for animations
	public void changeMode(int m){
		anim.SetInteger ("mode", m);
	}
}
