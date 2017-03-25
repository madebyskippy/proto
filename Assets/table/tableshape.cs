using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableshape : MonoBehaviour {

	//maybe make it so you click it and it stays?
	//for physics: if it "sleep" then it's stable

	[SerializeField] GameObject shadow;
	[SerializeField] GameObject outline;

	Vector3 initialScale;

	int count;
	int total;
	float interval = 1f;
	float time;

	bool isActive;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = Random.ColorHSV (10f/360f, 60f/360f, 0.3f, 0.8f, 100f/255f, 180f/255f);
		initialScale = transform.localScale;
		time = 0;
		total = Random.Range (5, 10);
		count = 0;
		shadow.SetActive (false);
		outline.SetActive (false);
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			time += Time.deltaTime;
			if (time > interval) {
				count++;
				if (count > total) {
					die ();
				}
				time = 0;
				transform.localScale = initialScale * Random.Range(0.75f,1.5f);
				transform.Rotate(new Vector3(0f,0f, Random.Range (-10f, 10f)));
				interval = Random.Range (0.75f, 1.5f);
			}
		}
	}

	void die(){
		Destroy (this.gameObject);
	}

	void OnMouseDown(){
//		Debug.Log ("clicked");
		shadow.SetActive (true);
		outline.SetActive (true);
		isActive = false;
	}

	public void freeze(){
		OnMouseDown ();
	}
}
