using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ra_kid : MonoBehaviour {

	[SerializeField] GameObject[] growths;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject[] getGrowths(){
		return growths;
	}
}
