using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tablelamp : MonoBehaviour {

	[SerializeField] tablemanager tm;
	[SerializeField] Sprite dark;

	SpriteRenderer sr;

	private int hp;

	// Use this for initialization
	void Start () {
		hp = 1;
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("hi");
		hp--;
		if (hp < 0) {
			sr.sprite = dark;
			sr.color = new Color (0.65f, 0.65f, 0.65f);
			Debug.Log ("deadddddddddd");
			tm.SendMessage ("dead");
		}
	}
}
