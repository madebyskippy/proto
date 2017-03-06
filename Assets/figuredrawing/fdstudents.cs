using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fdstudents : MonoBehaviour {

	/*
	 * this script randomizes student movement and plays the sound with it
	 */

	[SerializeField] GameObject z;
	[SerializeField] GameObject stud1;
	[SerializeField] GameObject stud2;

	private Animator zanim;
	private Animator stud1anim;
	private Animator stud2anim;

	private float probability = 0.01f;

	// Use this for initialization
	void Start () {
		zanim = z.GetComponent<Animator> ();
		stud1anim = stud1.GetComponent<Animator> ();
		stud2anim = stud2.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (0f, 1f) < probability) {
			zanim.Play ("zpause");
		}
		if (Random.Range (0f, 1f) < probability) {
			stud1anim.Play ("stud1pause");
		}
		if (Random.Range (0f, 1f) < probability) {
			stud2anim.Play ("stud2pause");
		}
	}

}
