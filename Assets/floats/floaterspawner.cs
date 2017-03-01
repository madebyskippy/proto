using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floaterspawner : MonoBehaviour {

	[SerializeField] GameObject floater;

	private int floaterMax = 50;
	private int floaterCount;

	// Use this for initialization
	void Start () {
		floaterCount = 0;
		generateFloater ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void generateFloater(){
		GameObject temp = Instantiate (floater);
		temp.transform.position = new Vector3 (Random.Range(-4f,4f), Random.Range(-4f,4f), 0f);
		temp.GetComponent<SpriteRenderer> ().color = Random.ColorHSV(0f, 1f, 0.25f, 0.5f, 0.75f, 1f);
		temp.GetComponent<TrailRenderer> ().startColor = temp.GetComponent<SpriteRenderer> ().color;
		temp.GetComponent<TrailRenderer> ().endColor = temp.GetComponent<SpriteRenderer> ().color;
		temp.transform.parent = this.transform;
		floaterCount++;
		if (floaterCount < floaterMax) {
			Invoke ("generateFloater", 0.25f);
		} else {
			Debug.Log ("all spawned");
		}
	}
}
