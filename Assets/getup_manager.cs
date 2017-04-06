using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class getup_manager : MonoBehaviour {

	[SerializeField] GameObject bar;
	[SerializeField] GameObject[] windows;

	private int scene;

	private float interval = 1f; //press every second
	private float time;

	private bool isDead;

	// Use this for initialization
	void Start () {
		time = 0;
		isDead = false;
		scene = 0; //start at the beginning
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene ("getup");
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (!isDead) {
			if (Input.GetKeyDown (KeyCode.X)) {
				time = 0;
			}

			time += Time.deltaTime;
			if (time > interval) {
				time = interval;
				//dead
				isDead = true;
				dead ();
			}

			bar.transform.localScale = new Vector3 ((0.1f) * (1f - time / interval), 0.01f, 1f);
		}
	}

	void dead(){
		Debug.Log ("dead");
		windows [scene].transform.GetChild (0).DOScaleX (35f, 5f);//windows[scene].transform.GetChild(0).transform.localScale.x*1.5f, 5f);
		windows [scene].transform.GetChild (1).DOScaleX (35f, 5f);//windows[scene].transform.GetChild(1).transform.localScale.x*1.5f, 5f);
		windows [scene].transform.GetChild (2).DOScaleY (15f, 5f);//[scene].transform.GetChild(2).transform.localScale.y*1.5f, 5f);
		windows [scene].transform.GetChild (3).DOScaleY (15f, 5f);//windows[scene].transform.GetChild(3).transform.localScale.y*1.5f, 5f);
	}

	void end(){
		Debug.Log ("end");
		windows [scene].transform.GetChild (0).DOScaleX (0f, 5f);
		windows [scene].transform.GetChild (1).DOScaleX (0f, 5f);
		windows [scene].transform.GetChild (2).DOScaleY (0f, 5f);
		windows [scene].transform.GetChild (3).DOScaleY (0f, 5f);
	}
}
