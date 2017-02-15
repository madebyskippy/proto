using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class meatcontrol : MonoBehaviour {
	[SerializeField] Text fill;

	[SerializeField] GameObject manager;
	private mf_manager managers;

	/*
	 * if the punchtimer is on, then you can do the actual gameplay
	 * --take in input, fill in the "fill"
	 * --if the fill is full, send message to manager
	 * 
	 * else you can just flop around
	 * --take in input, do nothing (but diff action than if you are filling)
	 * 
	 */


	// Use this for initialization
	void Start () {
		fill.text = "";
		managers = manager.GetComponent<mf_manager> ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 26; i++) {

			if (Input.GetKeyDown (KeyCode.A + i)) {
				if (managers.getIsPunching ())
					meatfill ((char)(97 + i));
				else
					wiggle ();
			}

		}
	}

	void wiggle(){
		Sequence sq = DOTween.Sequence();
		sq.Append(transform.DOScale(new Vector2(1.25f, 1.25f), 0.05f));
		sq.Append(transform.DOScale(new Vector2(1f, 1f), 0.05f));
	}

	void meatfill(char letter){
		fill.text += letter;
		managers.setFill (1);
	}
}
