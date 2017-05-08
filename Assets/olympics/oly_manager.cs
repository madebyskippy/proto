using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class oly_manager : MonoBehaviour {

	[SerializeField] GameObject background;
	[SerializeField] GameObject silhouette;
	[SerializeField] GameObject[] parents;

	[SerializeField] GameObject start;
	[SerializeField] GameObject end;
	[SerializeField] Text endText;
	[SerializeField] Image[] endchoices;
	[SerializeField] Image arrow;

	[SerializeField] oly_sport sport;

	List<GameObject[]> crowd;
	int layers = 4;
	Vector2 densityRange = new Vector2(5f,7f);
	Vector2 rangeX = new Vector2(-10f,10f);
	Vector2 rangeY = new Vector2(-4f,-1f);

	int mode; //0 1 or 2 (enter answer) or 3 (actual endgame state)

	int choice = 2;

	// Use this for initialization
	void Start () {
		crowd = new List<GameObject[]> ();
		for (int i = 0; i < layers; i++) {
			GameObject[] layer = new GameObject[Random.Range ((int)densityRange.x, (int)densityRange.y+1)];
			for (int j = 0; j < layer.Length; j++) {
				GameObject t = Instantiate (silhouette, new Vector3 (Random.Range(rangeX.x*2f,rangeX.y*2f), Random.Range(rangeY.x,rangeY.y), 0f), Quaternion.identity);
				t.transform.parent = parents [i].transform;
				t.GetComponent<SpriteRenderer> ().sortingOrder = i + 1;
				layer [j] = t;
			}
			crowd.Add (layer);
		}
		start.SetActive (true);
		end.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (mode == 3) {
				SceneManager.LoadScene ("olympics");
			} else {
				mode = 1;
				sport.setGoing (true);
				start.SetActive (false);
			}
		}if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("olympics");
		}
			
		float horiz = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");

		if (mode == 0 || mode == 1) {
			Vector3 t = background.transform.position;
			t.y -= 0.1f * vert;
			t.x += 0.1f * horiz;
			t.y = Mathf.Clamp (t.y, -1.5f, 0.5f);
			t.x = Mathf.Clamp (t.x, -3f, 3f);
			background.transform.position = t;

			for (int i = 0; i < parents.Length; i++) {
				t = parents [i].transform.position;
				t.y += 0.05f * (i + 1) * vert;
				t.x += 0.05f * (i + 1) * horiz;
				t.y = Mathf.Clamp (t.y, -1.5f, 3f);
				t.x = Mathf.Clamp (t.x, -8f, 8f);
				parents [i].transform.position = t;
			}
		} else if (mode == 2) {
			//end mode, move the hand
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				choice = Mathf.Max (choice - 1, 0);
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				choice = Mathf.Min (choice + 1, 4);
			}
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				enterchoice ();
			}
			arrow.GetComponent<RectTransform> ().localPosition = endchoices [choice].GetComponent<RectTransform> ().localPosition;
		} else {
		}
	}

	public void endRace(){
		mode = 2;
		end.SetActive (true);
		setupendmenu ();
	}

	void setupendmenu(){
		choice = 2;
		GameObject[] choices = sport.ath ();
		for (int i = 0; i < choices.Length; i++) {
			endchoices[i].sprite = choices [i].GetComponent<SpriteRenderer> ().sprite;
//			choices [i].transform.position = new Vector3(-5f + i*2f,2f,0f);
		}
	}

	void enterchoice(){
		mode = 3;
		if (choice == sport.getwinner ()) {
			arrow.color = Color.green;
			endText.text = "wow good eyes!\n(space to restart)";
			//waooo u were rite
		} else {
			arrow.color = Color.red;
			endText.text = "seems like you weren't watching close enough )-:\n(space to restart)";
			//u lose sucker
		}
	}
}
