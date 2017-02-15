using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour {

	public Camera cam;

	public Sprite[] people;
	public Sprite[] bubbs;

	public GameObject bubble;

	public string version;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<Camera> ();
//		bubble.GetComponent<SpriteRenderer> ().sprite = bubbs [Random.Range (0, 4)];
//		GetComponent<SpriteRenderer> ().sprite = people [Random.Range (0, 4)];
		version = "p" + (Random.Range (0, 4) + 1);
		GetComponent<Animator>().Play(version);
		int bub = Random.Range (0, 4) + 1;
		bubble.GetComponent<Animator>().Play("bubble"+bub);
		switch (bub) {
		case 1:
			bubble.transform.position += new Vector3 (0f, 0f, 0f);
			break;
		case 2:
			bubble.transform.position += new Vector3 (5.85f, -1.38f, 0f);
			break;
		case 3:
			bubble.transform.position += new Vector3 (6.44f, 0f, 0f);
			break;
		case 4:
			bubble.transform.position += new Vector3 (0.28f, -2.03f, 0f);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		Quaternion temp = cam.transform.rotation;
		temp.x = 0f;
		temp.z = 0f;
		transform.LookAt (transform.position + temp * Vector3.forward, temp * Vector3.up);
	}

}
