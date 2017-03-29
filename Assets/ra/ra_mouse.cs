using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ra_mouse : MonoBehaviour {

	[SerializeField] ra_manager manager;
	[SerializeField] AudioSource audio;

	private bool isOn;

	// Use this for initialization
	void Start () {
		isOn = false;	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		temp.z = 0f;
		temp.x = Mathf.Clamp (temp.x, -8f, 8f);
		temp.y = Mathf.Clamp (temp.y, -4.5f,4.5f);

		if (isOn)
			transform.position = temp;
	}

	void OnTriggerEnter2D(Collider2D col) {
//		Debug.Log ("hit");
//		Destroy (col.gameObject);
		if (isOn) {
			audio.Play ();
			GameObject temp = col.gameObject;
			float scale = temp.transform.localScale.x;
			if (scale < 0.1f) {
				scale = Random.Range (0.5f, 1.5f);
			}
			Sequence sq = DOTween.Sequence ();
			sq.Append (temp.transform.DOScale (0f, Random.Range (1f, 3f)));
			sq.Append (temp.transform.DOScale (scale, Random.Range (1f, 3f)).SetEase (Ease.InCirc));
			manager.addMask ();
		}
	}

	public void setMode(bool b){
		isOn = b;
		if (!isOn) {
			transform.position = new Vector3 (0f, 10f, 0f);
		}
	}
}
