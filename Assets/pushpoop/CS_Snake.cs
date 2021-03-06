﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Snake : MonoBehaviour {
	[SerializeField] GameObject myBodySample;
	[SerializeField] Vector2 myBodyDeltaPosition;
	[SerializeField] int myBodyTotal = 100;
	private List<GameObject> myBodyParts = new List<GameObject> ();

	[SerializeField] GameObject myBodyConnectionSample;
	private List<GameObject> myBodyConnections = new List<GameObject> ();

	// Use this for initialization
	void Start () {

		//Init Body

		GameObject t_bodyPartOne = Instantiate (myBodySample, this.transform.position, Quaternion.identity) as GameObject;
		t_bodyPartOne.transform.SetParent (this.transform);
		t_bodyPartOne.GetComponent<SpringJoint2D> ().enabled = false;
		myBodyParts.Add (t_bodyPartOne);

		for (int i = 1; i < myBodyTotal; i++) {
			GameObject t_bodyPart = Instantiate (myBodySample, this.transform.position, Quaternion.identity) as GameObject;
			t_bodyPart.transform.SetParent (this.transform);
			myBodyParts.Add (t_bodyPart);

			myBodyParts [i].transform.position = (Vector3)myBodyDeltaPosition + myBodyParts [i - 1].transform.position;
			//myBodyParts [i].GetComponent<HingeJoint2D> ().connectedAnchor = myBodyDeltaPosition;
			//myBodyParts [i].GetComponent<HingeJoint2D> ().connectedBody = myBodyParts [i - 1].GetComponent<Rigidbody2D> ();
			myBodyParts [i].GetComponent<SpringJoint2D> ().connectedAnchor = myBodyDeltaPosition;
			myBodyParts [i].GetComponent<SpringJoint2D> ().connectedBody = myBodyParts [i - 1].GetComponent<Rigidbody2D> ();
		}

		//Init Body Connection

		for (int i = 1; i < myBodyParts.Count; i++) {
			GameObject t_bodyConnection = Instantiate (myBodyConnectionSample, this.transform);
			myBodyConnections.Add (t_bodyConnection);

		}
			
	}
	
	// Update is called once per frame
	void Update () {
		//Update Body Connection

		for (int i = 0; i < myBodyConnections.Count; i++) {
			Vector2 t_direction = myBodyParts [i].transform.position - myBodyParts [i + 1].transform.position;
			Vector2 t_position = (myBodyParts [i].transform.position + myBodyParts [i + 1].transform.position) / 2;

			Quaternion t_quaternion = Quaternion.Euler (0, 0, 
				Vector2.Angle (Vector2.up, t_direction) * Vector3.Cross (Vector3.up, (Vector3)t_direction).normalized.z);

			myBodyConnections [i].transform.position = t_position;
			myBodyConnections [i].transform.rotation = t_quaternion;
			myBodyConnections [i].transform.localScale = new Vector3 (1, t_direction.magnitude, 1);
		}

//		UpdateAnchor ();
	}


	public List<GameObject> GetBodyParts () {
		return myBodyParts;
	}


}
