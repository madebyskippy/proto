using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fdrawing : MonoBehaviour {

	/*
	 * grid is 100x55
	 */

	public float totaltime = 3 * 60f;
	private float currenttime;

	[SerializeField] GameObject gridscript;
	[SerializeField] GameObject hand;
	private List<GameObject> grids;
	private bool mousedown;

	// Use this for initialization
	void Start () {
		mousedown = false;
		grids = gridscript.GetComponent<CS_GridSetup> ().getgrid ();
		currenttime = 0;
	}

	// Update is called once per frame
	void Update () {

		currenttime -= Time.deltaTime;
		if (currenttime > totaltime) {
			//game end
		}

		if (Input.GetMouseButtonDown (0)) {
			mousedown = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			mousedown = false;
		}


		if (mousedown) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Player") {
					hit.collider.gameObject.GetComponent<SpriteRenderer> ().color = Color.black;
					Vector3 pos = hit.collider.gameObject.transform.position;
					pos.x += 3.85f;
					pos.y -= 4.65f;

					hand.transform.position = pos;
				}
			}
		}
	}
}
