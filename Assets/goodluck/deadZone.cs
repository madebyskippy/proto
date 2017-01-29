using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZone : MonoBehaviour {
	public GameObject manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
//		Debug.Log ("something hit bottom");
		manager.GetComponent<manager> ().increaseScore();
		Destroy (collider.gameObject);
	}
}
