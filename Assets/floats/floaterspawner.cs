using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floaterspawner : MonoBehaviour {

	[SerializeField] GameObject floater;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 50; i++) {
			GameObject temp = Instantiate (floater);
			temp.transform.position = new Vector3 (Random.Range(-4f,4f), Random.Range(-4f,4f), 0f);
			temp.GetComponent<SpriteRenderer> ().color = Random.ColorHSV(0f, 1f, 0.25f, 0.5f, 0.5f, 1f);
			temp.GetComponent<TrailRenderer> ().startColor = temp.GetComponent<SpriteRenderer> ().color;
			temp.GetComponent<TrailRenderer> ().endColor = temp.GetComponent<SpriteRenderer> ().color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
