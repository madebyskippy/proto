using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zamscene : MonoBehaviour {

	[SerializeField] int num;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void needaction(){
		GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);
	}

	public void doaction(bool shouldve){
		GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		if (shouldve) {
		} else {
		}
	}

	public void failaction(){
		GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
	}

	public int getnum(){
		return num;
	}
}
