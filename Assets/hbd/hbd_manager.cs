using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class hbd_manager : MonoBehaviour {

	/*
	 * floater color range
	 * design the diff scenes
	 */

	[SerializeField] Text text;
	[SerializeField] Image white;
	[SerializeField] GameObject scenecontainer;
	private GameObject[] scenes;

	private string[] letter = {"a letter:",
		"\t\t\t\ti've been thinking a lot about tenderness,\n\n\n\n\n\n\n\n\n\n\n\n\n",
		"what it meant to sit with me\n\n\tin all my terror and grief\n\n\n \t\t\tthat november night\n\t\t\t\t in the bathroom.",
		"what it meant to connect with my humanity and so, so patiently hold all of the parts of me struggling to understand my identity after trauma.",
		"when i have trouble remembering the tenderness in my life, these are the first moments that come to mind.",
		"becoming is hard.",
		"this summer i hope you remember to trust yourself, to hold yourself in all your worth.",
		"i hope that your becomings in both mind and body feel like home at every stage.",
		"i hope that you value your tenderness, your creativity, your fluidity, your empathy, your quiet visceral humming life.",
		"i hope the world gives you back a hundred-hundredfold what you breathe into it.",
		"i hope you believe wholeheartedly you deserve all of it when it does. ",
		"i hope it gets easier.",
		"i hope when you look back, you remember you're moving forward.",
		"happy second birthday."};
	private int currenttext;

	private bool istransitioning; //lmfao the irony

	// Use this for initialization
	void Start () {
		currenttext = 0;
		scenes = new GameObject[scenecontainer.transform.childCount];
		int i = 0;
		foreach (Transform child in scenecontainer.transform) {
			scenes[i] = child.gameObject;
			scenes [i].SetActive (false);
			i++;
		}
//		InvokeRepeating ("changetext", 0f, 1f);
		istransitioning=false;
		changetext();
	}
	
	// Update is called once per frame
	void Update () {
		//temporary !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (currenttext < letter.Length && !istransitioning)
				transitiontext ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("hbd");
		}
	}

	void transitiontext(){
		float time = 1f;
		fadetext (0.8f, 0f, time);
//		DOTween.ToAlpha (() => white.color, x => white.color = x, 1f,time);
		fadewhite (0f, 1f, time);
		scenes [currenttext-1].SetActive (false);
		Invoke ("changetext",time);
	}
	void changetext(){
		fadetext (0f,0.8f,1f);
//		DOTween.ToAlpha (() => white.color, x => white.color = x, 0f,3f);
		fadewhite (1f, 0f, 3f);
		istransitioning = false;
		text.text = letter [currenttext];
		scenes [currenttext].SetActive (true);
		currenttext++;
	}
	void fadetext(float i, float o, float time){
		istransitioning = true;
		Color temp = text.color;
		temp.a = i;
		text.color = temp;
		DOTween.ToAlpha (() => text.color, x => text.color = x, o,time);
	}
	//because passing the color didn'g work? ?!?! ?!?1
	void fadewhite(float i, float o, float time){
		istransitioning = true;
		Color temp = white.color;
		temp.a = i;
		white.color = temp;
		DOTween.ToAlpha (() => white.color, x => white.color = x, o,time);
	}
}
