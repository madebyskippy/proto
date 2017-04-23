using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yelling : MonoBehaviour {

	[SerializeField] Animator small;
	[SerializeField] GameObject rings;

	float pump_timer;
	float tire_timer;

	float maxyell = 3.5f;
	float power;

	bool pumping;

	// Use this for initialization
	void Start () {
		pump_timer = 0;
		pumping = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("yelling");
		}if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			small.Play ("pumpup");
			pump_timer = 0;
			pumping = true;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			pumping = false;
			power = Mathf.Clamp (pump_timer, 0f, maxyell);
			yell ();
		}

		if (pumping) {
			pump_timer += Time.deltaTime;
		}
	}

	void yell(){
		Debug.Log (pump_timer);
		small.Play ("yell");
		Invoke ("tire", power);
		rings.GetComponent<rings> ().changeSize (5f+power*2f);
		rings.GetComponent<rings> ().changePause (0.1f + power);
		rings.GetComponent<rings> ().begin ();
	}

	void tire(){
		small.Play ("idle");
	}
}
