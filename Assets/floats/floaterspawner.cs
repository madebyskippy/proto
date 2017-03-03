using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floaterspawner : MonoBehaviour {
	//put a window around it

	[SerializeField] GameObject floater;
	[SerializeField] GameObject parent;

	private int floaterMax = 50;
	private int floaterCount;
	private float floatinterval = 0.25f;
	private List<GameObject> floats;

	// Use this for initialization
	void Start () {
		floats = new List<GameObject> ();
		floaterCount = 0;
		generateFloater ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		temp.z = 0f;
		if (temp.x > 7) {
			temp.x = 7;
		} if (temp.x < -7) {
			temp.x = -7;
		} if (temp.y > 7) {
			temp.y = 7;
		} if (temp.y < -7) {
			temp.y = -7;
		}
		transform.position = temp;
	}

	void generateFloater(){
		GameObject temp = Instantiate (floater);
		temp.transform.position = transform.position;//new Vector3 (Random.Range(-4f,4f), Random.Range(-4f,4f), 0f);
		temp.GetComponent<SpriteRenderer> ().color = Color.white;//Random.ColorHSV(0f, 1f, 0.15f, 0.25f, 0.75f, 1f);
		temp.GetComponent<TrailRenderer> ().startColor = temp.GetComponent<SpriteRenderer> ().color;
		temp.GetComponent<TrailRenderer> ().endColor = temp.GetComponent<SpriteRenderer> ().color;
		temp.transform.parent = parent.transform;
		temp.GetComponent<floater> ().setOrigin (this.gameObject);
		floaterCount++;
		if (floaterCount < floaterMax) {
			Invoke ("generateFloater", floatinterval);
		} else {
			Debug.Log ("all spawned");
		}
	}
}
