using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSchara : MonoBehaviour {

	private float speed=0.15f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (Input.GetAxis ("Horizontal")*speed, 0f, Input.GetAxis("Vertical")*speed);
	}
}
