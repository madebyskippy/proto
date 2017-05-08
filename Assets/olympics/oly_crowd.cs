using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oly_crowd : MonoBehaviour {

	[SerializeField] Sprite[] sprites;

	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		sr.sprite = sprites [Random.Range (0, sprites.Length)];
		if (Random.Range (0f, 1f) < 0.5f) {
			sr.flipX = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
