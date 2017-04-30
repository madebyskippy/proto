using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class poke : MonoBehaviour {

	[SerializeField] GameObject dud;

	GameObject[,] dudes;

	GameObject star;

	int numsteps = 3; //kind of like puzzle difficulty
	List<Vector2> boops;

	private float topX = -6.1f;
	private float topY = 4.5f;
	private int rows = 4;
	private int cols = 8;

	// Use this for initialization
	void Start () {
		dudes = new GameObject[rows,cols];
		boops = new List<Vector2> ();

		for (int i = 0; i < cols; i++) {
			for (int j = 0; j < rows; j++) {
				Vector3 pos = new Vector3 (topX + (1.75f*i), topY - (2.75f*j), 0f);
				GameObject t = Instantiate (dud, pos, Quaternion.identity);
				t.GetComponent<dude> ().Init (j,i);
				dudes [j, i] = t;
			}
		}

		generate_puzzle ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene("poke");
		}

		if (Input.GetKeyDown(KeyCode.Space)){
			Debug.Log ("reset");
			reset();
		}

	}

	public void boop(int r, int c){
		dudes [r, c].GetComponent<Animator> ().Play ("poke");
		bool left = false;
		bool right = false;
		if (c <= 0)
			left = true;
		if (c >= cols-1)
			right = true;
		
		if (r > 0) {
			if (!left)
				dudes [r - 1, c - 1].GetComponent<Animator> ().Play ("down-right");
			dudes [r - 1, c].GetComponent<Animator> ().Play ("down-top");
			if (!right)
				dudes [r - 1, c + 1].GetComponent<Animator> ().Play ("down-left");
		}

		if (!left)
			dudes [r,c-1].GetComponent<Animator> ().	Play ("right-side");
		if (!right)
			dudes [r,c+1].GetComponent<Animator> ().	Play ("left-side");
		
		if (r < rows-1) {
			if (!left)
				dudes [r + 1, c - 1].GetComponent<Animator> ().Play ("up-right");
			dudes [r + 1, c].GetComponent<Animator> ().Play ("up-top");
			if (!right)
				dudes [r + 1, c + 1].GetComponent<Animator> ().Play ("up-left");
		}
	}

	public void click(int r, int c){
		Debug.Log("poke");
		boop(r,c);
		boops.Add (new Vector2 (r, c));
	}

	void generate_puzzle(){
		int r, c;
		for (int i = 0; i < numsteps; i++) {
			r = Random.Range (0, rows);
			c = Random.Range (0, cols);
			boop (r, c);
//			Debug.Log (r + ", " + c);
			boops.Add (new Vector2 (r, c));
		}

		star = dudes [Random.Range (0, rows), Random.Range (0, cols)];
		star.GetComponent<Animator> ().Play ("needy");
	}

	void reset(){
		Debug.Log ("resetting");
		for (int i = 0; i < cols; i++) {
			for (int j = 0; j < rows; j++) {
				dudes [j, i].GetComponent<dude> ().reset();
				if (boops.Contains (new Vector2 (j, i))) {
					Debug.Log ("booping "+j+", "+i);
					boop (j, i);
				}
			}
		}
	}

	bool check(){
		if (boops.Count > 1) {
			return false;
		}
		//check if the star is booped
		return true;
	}
}
