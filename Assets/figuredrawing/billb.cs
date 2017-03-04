using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billb : MonoBehaviour {

	public Camera cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<Camera> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		Quaternion temp = cam.transform.rotation;
		temp.x = 0f;
		temp.z = 0f;
		transform.LookAt (transform.position + temp * Vector3.forward, temp * Vector3.up);
	}
}
