using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ra_manager : MonoBehaviour {

	[SerializeField] GameObject mask;

	private List<GameObject> masks;

	// Use this for initialization
	void Start () {
		masks = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene ("ra");
		}

		for (int i = 0; i < masks.Count; i++) {
			//jitter them
			Vector3 incr = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.05f,0.05f),0f);
			incr.x = Mathf.Clamp (incr.x, -8f, 8f);
			incr.y = Mathf.Clamp (incr.y, -4.5f,4.5f);
			masks[i].transform.position += incr;
		}
	}

	public void addMask(){
		GameObject temp = Instantiate (mask);

		Vector3 pos = new Vector3 (Random.Range(-8f,8f), Random.Range(-4.5f,4.5f), 0f);
		temp.transform.position = pos;

		Vector3 scale = temp.transform.localScale;
		scale.x = Random.Range (3f, 10f);
		scale.y = Random.Range (0.075f, 0.2f);
//		temp.transform.localScale = scale;
		temp.transform.localScale=Vector3.zero;
		temp.transform.DOScale (scale, 0.25f);//.SetEase(Ease.InCirc);

		masks.Add (temp);
	}
}
