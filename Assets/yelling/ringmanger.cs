using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringmanger : MonoBehaviour {

	[SerializeField] GameObject[] rings;

	int max;
	float size;
	float pause;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			jostle ();
			rings [0].GetComponent<rings> ().begin ();
//			rings [1].GetComponent<rings> ().begin ();
//			rings [2].GetComponent<rings> ().begin ();
		}
	}

	void jostle(){
		for (int i = 0; i < rings.Length; i++) {
			rings [i].transform.position += new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0);
			Vector3 temp = rings [i].transform.position;
			if (rings [i].transform.position.x > 8) {
				temp.x = 8;
			}if (rings [i].transform.position.x < -8) {
				temp.x = -8;
			}if (rings [i].transform.position.y < -4) {
				temp.y = -4;
			}if (rings [i].transform.position.y > 4) {
				temp.y = 4;
			}
			rings [i].transform.position = temp;

			rings [i].transform.localScale = Vector3.one - Vector3.one * Random.Range (.1f, .3f) * i;
		}
	}


	public void changeMax(float val){
		max = (int)val;
		foreach (GameObject r in rings) {
			r.GetComponent<rings> ().changeMax (val);
		}
	}
	public void changeSize(float val){
		size = val;
		foreach (GameObject r in rings) {
			r.GetComponent<rings> ().changeSize (val);
		}
	}
	public void changePause(float val){
		pause = val;
		foreach (GameObject r in rings) {
			r.GetComponent<rings> ().changePause (val);
		}
	}
}
