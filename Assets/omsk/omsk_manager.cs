using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class omsk_manager : MonoBehaviour {

	[SerializeField] Camera cam;
	
	[SerializeField] GameObject flower;
	[SerializeField] Sprite[] plant1;
	[SerializeField] Sprite[] plant2;
	[SerializeField] Sprite[] plant3;

	[SerializeField] GameObject ground;
	[SerializeField] GameObject chara;

	[SerializeField] GameObject welcome;
	[SerializeField] GameObject bird;
	[SerializeField] GameObject stay;
	[SerializeField] GameObject instruc;
	private int instrucCount;


	private float radius;
	private float shrinkRate = 0.95f;
	private float minRadius = 2.5f;

	private int maxPlants; //depends on radius
	private List<GameObject> plants;

	// Use this for initialization
	void Start () {
		instrucCount = 0;
		instruc.SetActive (true);
		welcome.SetActive (false);
		bird.SetActive (false);
		stay.SetActive (false);
		radius = ground.transform.localScale.x * 0.5f;
		maxPlants = (int)(10f * radius);
		chara.GetComponent<omsk_chara> ().setRadius (radius);
		plants = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			SceneManager.LoadScene ("omsk");
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	//clears plants from the part that isn't within the radius
	void clearPlantsOutside(){
		int index = 0;
		for (int i = 0; i < plants.Count; i++) {
			float dist = Vector3.Distance (plants [i].transform.position, Vector3.zero);
			if (dist > radius) {
				GameObject temp = plants[i];
				plants.RemoveAt (i);
				Destroy (temp);
			} else {
				index++;
			}
		}
	}

	void clearAllPlants(){
		int size = plants.Count;
		for (int i = 0; i < size; i++) {
			GameObject temp = plants[0];
			plants.RemoveAt (0);
			Destroy (temp);
		}
	}

	public void bump(){
		radius *= shrinkRate;
		clearPlantsOutside ();
		if (radius > minRadius) {
			chara.GetComponent<omsk_chara> ().setRadius (radius);
			ground.transform.localScale = Vector3.one * radius * 2;
		} else {
			//end game, you're allowed out of the circle now, and now the circle dies
			stay.SetActive(true);
			cam.backgroundColor = new Color (0.5f,0.5f,0.5f,1f);
			bird.SetActive (true);
			welcome.SetActive (false);
			instruc.SetActive (false);
			clearAllPlants();
			ground.GetComponent<SpriteRenderer>().color = Color.black;
			chara.GetComponent<omsk_chara> ().setFree (true);
		}
	}

	public bool plant(){
		if (plants.Count < maxPlants) {
//			Debug.Log (plants.Count);
			GameObject f = Instantiate (flower, chara.transform.position, Quaternion.identity);
			f.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().sprite = plant1 [Random.Range(0,plant1.Length)];
			f.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = Random.ColorHSV(0f, 1f, 0.15f, 0.25f, 0.75f, 1f);
			f.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().sprite = plant2 [Random.Range(0,plant2.Length)];
			f.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = Random.ColorHSV(0f, 1f, 0.15f, 0.25f, 0.75f, 1f);
			f.transform.GetChild (2).gameObject.GetComponent<SpriteRenderer> ().sprite = plant3 [Random.Range(0,plant3.Length)];
			f.transform.GetChild (2).gameObject.GetComponent<SpriteRenderer> ().color = Random.ColorHSV(0f, 1f, 0.15f, 0.25f, 0.75f, 1f);
			f.transform.position = new Vector3 (f.transform.position.x, 0f, f.transform.position.z);
			f.transform.localScale = Vector3.one * Random.Range (0.5f, 1.75f);
			float s = f.transform.localScale.x;
			f.transform.localScale = Vector3.one * 0.1f;
			f.transform.DOScale (s, 1f);
			plants.Add (f);
			instrucCount++;
			if (instrucCount > 2) {
				instruc.SetActive (false);
			}
			return true;
		} else {
			welcome.SetActive (true);
			instruc.SetActive (false);
			return false;
		}
	}
}
