using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class zammanager : MonoBehaviour {

	/*
	 * missing: the augers, snowbreaker
	 * 
	 * key controls: Q C T J P
	 * press once to do the action. 
	 * 
	 * get instructions on what to do on the side (speech bubble?)
	 * 
	 * if you don't do it in time, FOV zooms and it gets a little wobbly
	 * 
	 * you can either fail (supervisor takes over)
	 * or you can "win" but have a warped sense
	 * 
	 */

	[SerializeField] Text instruc;

	[SerializeField] GameObject[] scenes;

	int mode = 0; //0 is start, 1 is gameplay, 2 is bad end, 3 is good end

	bool[] 	actions = 		{false,false,false,false,false};
	float[] actionrate = 	{0.01f,0.001f,0.0001f,0.0025f,0.0075f};
	float[]	actiondur = 	{1f,1f,1f,1f,1f};
	float[] actiontimer = 	{0f,0f,0f,0f,0f};
	int[] 	keys = 			{16, 2, 19, 9, 15};

	// Use this for initialization
	void Start () {
		instruc.text = "time to drive the zamboni.\n\nQ to steer\nC for gas\nT to switch gears or adjust rpm\nJ to adjust the blade\nP to control the water";
		instruc.text += "\n\npress the gas (augers?) to start.";
		//press _ _ to lower the conditioner and start the augers and start.
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			SceneManager.LoadScene ("zamboni");
		}if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		for (int i = 0; i < keys.Length; i++) {
			if (Input.GetKeyDown (KeyCode.A + keys [i])) {
				if (mode == 0 && i == 1) {
					mode = 1;
					instruc.text = "don't forget to pay attention to everything";
				}
				if (mode == 1) {
					//player gameplay
					doaction(i);
				}
			}
		}
		if (mode == 1) {
			//g-g-g-g-g-g--gg-g---aaaameplay
			generateaction();
			checkactions ();
		}
	}

	void generateaction(){
		for (int i = 0; i < actions.Length; i++) {
			if (!actions [i]) {
				if (Random.Range (0f, 1f) < actionrate [i]) {
					actiontimer [i] = 0f;
					actions [i] = true;
					scenes [i].GetComponent<zamscene>().needaction();
					Debug.Log ("action " + i + " needed");
				}
			}
		}
	}

	void checkactions(){
		for (int i = 0; i < actions.Length; i++) {
			if (actions [i]) {
				actiontimer [i] += Time.deltaTime;
				if (actiontimer [i] > actiondur [i]) {
					//you failed
					scenes [i].GetComponent<zamscene>().failaction();
					Debug.Log ("action " + i + " failed");
					actions [i] = false;
				}
			}
		}
	}

	void doaction(int a){
		Debug.Log ("action " + a + " done");
		scenes [a].GetComponent<zamscene>().doaction(actions[a]);
	}
}
