using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropEnemies : MonoBehaviour {

	public GameObject prefab;
	public GameObject manager;
	public GameObject player;


	float timeLeft;
	float interval = 0.25f; //in sec

	int count;

	bool isDropping;

	// Use this for initialization
	void Start () {
		timeLeft = interval;
		count = 0;
		isDropping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDropping) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				//generate
				timeLeft = interval;
				count++;
				GameObject temp = Instantiate (prefab, transform.position, Quaternion.identity) as GameObject;
				float randomPosition = Random.Range (-3.8f, 3.8f);
				if (count % 5 == 0) {
					randomPosition = player.transform.position.x;
				}
				temp.transform.position = new Vector3 (randomPosition, temp.transform.position.y, 0f);
			}
		}
	}

	public void setIsDropping(bool set){
		isDropping = set;
	}


}
