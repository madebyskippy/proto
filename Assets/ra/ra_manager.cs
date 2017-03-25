using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ra_manager : MonoBehaviour {

	[SerializeField] GameObject[] kids;
	[SerializeField] GameObject fillPrefab;

	private List<GameObject>[] fillings;

	// Use this for initialization
	void Start () {
		fillings = new List<GameObject>[kids.Length];
		for (int i = 0; i < kids.Length; i++) {
			fillings [i] = new List<GameObject> ();
			fillings[i].Add(kids[i].transform.GetChild(0).gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
//			poof();
		}
	}

	void poof(){
		float size = fillings [0] [0].transform.localScale.x;
		GameObject parent = fillings [0] [0];
		for (int i = 0; i < 10; i++) {
			GameObject temp = Instantiate (fillPrefab);

			parent = fillings[0] [Random.Range (0, fillings[0].Count)];//-(fillings[0].Count/2))];
			float xrad = parent.transform.localScale.x * (0.25f + Random.Range(0f,0.25f)) * (Random.Range(0,2)*2-1);
			float x = Random.Range(0,xrad); //the radius already has randomness in it
			float y = Mathf.Sqrt(xrad*xrad - x*x)*(Random.Range(0,2)*2-1);
			//		Debug.Log (y);
			temp.transform.position = parent.transform.position + new Vector3(x,y,0f);

			temp.transform.localScale = Vector3.one * (0.75f) * size;
			float scale = temp.transform.localScale.x;
			temp.transform.localScale = Vector3.zero;
			temp.transform.DOScale (scale, Random.Range(0.5f,1f)*(i+1));
		}
	}
}
