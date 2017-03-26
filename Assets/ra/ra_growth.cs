using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ra_growth : MonoBehaviour {

	[SerializeField] GameObject fill;


	private List<GameObject> fillings;

	// Use this for initialization
	void Start () {
		fillings = new List<GameObject> ();
		fillings.Add (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			poof();
		}
	}

	void poof(){
		float size = transform.localScale.x;
		GameObject parent = this.gameObject;
		for (int i = 0; i < 10; i++) {
			GameObject temp = Instantiate (fill);

			parent = fillings [Random.Range (0, fillings.Count)];//-(fillings[0].Count/2))];
			float xrad = parent.transform.localScale.x * (0.25f + Random.Range(0f,0.25f)) * (Random.Range(0,2)*2-1);
			float x = Random.Range(0,xrad); //the radius already has randomness in it
			float y = Mathf.Sqrt(xrad*xrad - x*x)*(Random.Range(0,2)*2-1);
			//		Debug.Log (y);
			temp.transform.position = parent.transform.position + new Vector3(x,y,0f);

			temp.transform.localScale = Vector3.one * Random.Range(0.75f,1.5f) * size;
			float scale = temp.transform.localScale.x;
			temp.transform.localScale = Vector3.zero;
			Sequence sq = DOTween.Sequence ();
			sq.Append (temp.transform.DOScale (0f, 1f * i));
			sq.Append (temp.transform.DOScale (scale, Random.Range (1f, 3f)).SetEase(Ease.InCirc));//*(i+1)));
		}
	}
}
