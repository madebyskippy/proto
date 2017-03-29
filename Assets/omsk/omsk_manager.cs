using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class omsk_manager : MonoBehaviour {
	
	[SerializeField] GameObject flower;

	[SerializeField] GameObject ground;
	[SerializeField] GameObject chara;


	private float radius;
	private float shrinkRate = 0.95f;
	private float minRadius = 2.5f;

	private int maxPlants; //depends on radius
	private List<GameObject> plants;

	// Use this for initialization
	void Start () {
		radius = ground.transform.localScale.x * 0.5f;
		maxPlants = (int)(10f * radius);
		chara.GetComponent<omsk_chara> ().setRadius (radius);
		plants = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene ("omsk");
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	//clears plants from the part that isn't within the radius
	void clearPlants(){
		int size = plants.Count;
		for (int i = 0; i < size; i++) {
			//check if it's outside radius (check distance to zerozero)
			//if yes, deleeeete
			//i have to figure out how to index this array.
		}
	}

	public void bump(){
		radius *= shrinkRate;
		if (radius > minRadius) {
			chara.GetComponent<omsk_chara> ().setRadius (radius);
			ground.transform.localScale = Vector3.one * radius * 2;
		} else {
			//end game, you're allowed out of the circle now, and now the circle dies
			ground.GetComponent<SpriteRenderer>().color = Color.black;
			chara.GetComponent<omsk_chara> ().setFree (true);
		}
	}

	public bool plant(){
		if (plants.Count < maxPlants) {
			Debug.Log (plants.Count);
			GameObject f = Instantiate (flower, chara.transform.position, Quaternion.identity);
			plants.Add (f);
			return true;
		} else {
			return false;
		}
	}
}
