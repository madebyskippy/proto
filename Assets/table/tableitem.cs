using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableitem : MonoBehaviour {

	[SerializeField] tablemanager manager;
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
		if (!manager.ended) {
			Vector3 pos = transform.position;//new Vector3 (0f, 2.5f, 0f);
			GameObject temp = Instantiate(abs,pos,Quaternion.identity);
		}
	}
}
