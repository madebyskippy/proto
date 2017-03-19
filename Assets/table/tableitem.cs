using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableitem : MonoBehaviour {

//	[SerializeField] Sprite abs;
	[SerializeField] GameObject abs;

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown(){
		Vector3 pos = transform.position;//new Vector3 (0f, 2.5f, 0f);
		GameObject temp = Instantiate(abs,pos,Quaternion.identity);
		temp.GetComponent<SpriteRenderer> ().color = Random.ColorHSV (0f, 1f, 0.15f, 0.25f, 0.75f, 1f);
	}
}
