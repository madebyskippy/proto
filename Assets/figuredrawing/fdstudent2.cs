﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fdstudent2 : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void donepause(){
		anim.SetBool ("todraw", true);
	}
}
