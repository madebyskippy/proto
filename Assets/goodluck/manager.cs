using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour {

	public Text scoreText;
	public Image[] lifePix;
	public Text title;

	public GameObject buddy;
	public GameObject buddies;
	public Sprite[] buds;

	public GameObject dropper;

	int score;
	int life;
	int maxLife = 3;

	float interval = 2f; //in seconds
	float timeLeft;
	float initialDelay = 2f; //in seconds

	int mode;

	// Use this for initialization
	void Start () {
		score = 0;
		life = maxLife;
		timeLeft = initialDelay;
		mode = 0;
		title.text = "wow! you just happen to be the best at avoiding falling brown circles. let's see you in action.";
		title.text += "\n \n (press space to start)";
		title.text += "\n (and left/right to move)";
		dropper.GetComponent<dropEnemies>().setIsDropping(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (mode == 0) {
			//start
			if (Input.GetKey(KeyCode.Space)){
				mode = 1;
				title.text = "";
				dropper.GetComponent<dropEnemies>().setIsDropping(true);
			}
		} else if (mode == 1) {
			//game
			if (life <= 0) {
				gameOver ();
			}

			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				interval *= .9f;
				timeLeft=interval;
				GameObject temp = Instantiate(buddy,buddies.transform.position,Quaternion.identity) as GameObject;
				temp.transform.SetParent (buddies.transform);
				float randomPositionX = Random.Range (-360f, 360f);
				float randomPositionY = Random.Range (-200f, 250);
				//			temp.transform.position = new Vector3 (randomPositionX, randomPositionY, 0f);
				temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomPositionX, randomPositionY);
				temp.transform.localScale = new Vector3 (0.55f, 0.55f, 1f);
				int b = (int)Random.Range (0, 4);
				temp.GetComponent<Image> ().sprite = buds [b];
				temp.GetComponent<Image> ().GetComponent<Image> ().SetNativeSize ();
			}
		} else {
			//end
			if (Input.GetKey(KeyCode.Space)){
				SceneManager.LoadScene ("goodluck");
			}
		}

		//restart code
		if (Input.GetKey(KeyCode.R)){
			SceneManager.LoadScene ("goodluck");
		}
	}

	public void increaseScore(){
		score++;
		if (mode==1)
			scoreText.text = "score: " + score;
	}

	public void decreaseLife(){
		life--;
		lifePix[life].GetComponent<Image>().enabled=false;
	}

	public void gameOver(){
		//TODO
		dropper.GetComponent<dropEnemies>().setIsDropping(false);
		Debug.Log ("gameover");
		mode = 2;
		scoreText.text = "";
		buddies.SetActive (false);
		title.text = "woooww, you avoided "+score+" pieces!!! \n sooo amaaaaazing. \n \n (press space to start over)";
	}
}
