using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arcs : MonoBehaviour {

	//color all squares without touching any black dots 

	[SerializeField] Vector2 start;

	[SerializeField] GameObject l;
	[SerializeField] GameObject circ;
	[SerializeField] GameObject poly;
	[SerializeField] Material m;

	[SerializeField] GameObject mouse;

	GameObject[,] colliders;
	GameObject[,] circles;
	Vector2[,] grid;
	Vector2[,] changes;
	int r = 10;
	int c = 10;

	LineRenderer[] lr_r;
	LineRenderer[] lr_c;
	Vector3[][] rows;
	Vector3[][] cols;

	bool gameover;
	bool win;

	// Use this for initialization
	void Start () {
		gameover = false;
		win = false;
		grid = new Vector2[r, c];
		changes = new Vector2[r, c];
		circles = new GameObject[r, c];
		colliders = new GameObject[r-1, c-1];
		lr_r = new LineRenderer[r];
		lr_c = new LineRenderer[c];
		rows = new Vector3[r][];
		cols = new Vector3[c][];
		for (int i = 0; i < r; i++) {
			GameObject t = Instantiate(l);
			rows [i] = new Vector3[c];
			lr_c [i] = t.GetComponent<LineRenderer> ();

//			GameObject t1 = Instantiate(l);
//			cols [i] = new Vector3[r];
//			lr_r [i] = t1.GetComponent<LineRenderer> ();

			for (int j = 0; j < c; j++) {
				grid [i, j] = new Vector2 (start.x + i, start.y - j);
				changes [i, j] = Vector2.zero;
				circles[i,j] = Instantiate (circ, grid [i, j], Quaternion.identity);
				if (i>0 && j<c-1)
					colliders [i-1, j] = Instantiate (poly, grid [i, j], Quaternion.identity);

				lr_c [i].SetPosition (j,new Vector3(grid[i,j].x,grid[i,j].y,0f));
				rows [i][j] = grid[i,j];

//				lr_r [i].SetPosition (j,new Vector3(grid[j,i].x,grid[j,i].y,0f));
//				cols [i][j] = grid [j,i];
			}
		}

		for (int j = 0; j < c; j++) {
			GameObject t = Instantiate(l);
			cols [j] = new Vector3[r];
			lr_r [j] = t.GetComponent<LineRenderer> ();
			for (int i = 0; i < r; i++) {
				lr_r [j].SetPosition (i,new Vector3(grid[i,j].x,grid[i,j].y,0f));
				cols [j][i] = grid [i, j];
			}
		}

		adjustpoly ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene ("arcs");
		}if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		temp.z = 0;
		mouse.transform.position = temp;

		if (!gameover && !win) {
			for (int j = 0; j < c; j++) {
				for (int i = 0; i < r; i++) {
					changes [i, j] = Vector2.one * Random.Range (-0.01f, 0.01f);
					edit (i, j, grid [i, j] + changes[i,j]);

				}
			}

			showlines ();
			placecirc ();
			adjustpoly ();
		}
		
	}

	void edit(int x, int y, Vector2 p){
		grid [x, y] = p;
		rows [x] [y] = grid [x,y];
		cols [y] [x] = grid [x,y];
	}

	void showlines(){
		for (int i = 0; i < r; i++) {
			lr_c [i].SetPositions (rows [i]);
			lr_r [i].SetPositions (cols [i]);
		}
	}

	void placecirc(){
		for (int j = 0; j < c; j++) {
			for (int i = 0; i < r; i++) {
				circles[i,j].transform.position=grid[i,j];
			}
		}
	}

	void adjustpoly(){
		for (int i = 1; i < r; i++) {
			for (int j = 0; j < c-1; j++) {
				GameObject o = colliders [i - 1, j];
				Vector2[] v2;
				Vector3[] v3;
				v2 = o.GetComponent<PolygonCollider2D> ().points;

				v2 [0] = o.transform.InverseTransformPoint(grid [i-1,	j+1]);//-Vector2.one;
				v2 [1] = o.transform.InverseTransformPoint(grid [i,		j]	);//-Vector2.one;
				v2 [2] = o.transform.InverseTransformPoint(grid [i,		j+1]);//-Vector2.one;
				v2 [3] = o.transform.InverseTransformPoint(grid [i-1,	j]	);//-Vector2.one;

				o.GetComponent<PolygonCollider2D> ().points = v2;

				if (i == 1 && j == 0) {
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.vertices[0]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.vertices[1]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.vertices[2]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.vertices[3]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.triangles[0]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.triangles[1]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.triangles[2]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.triangles[3]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.triangles[4]);
					Debug.Log (colliders [i - 1, j].GetComponent<MeshFilter> ().mesh.triangles[5]);
				}
				v3 = o.GetComponent<MeshFilter> ().mesh.vertices;
				v3 [0] = o.transform.InverseTransformPoint((Vector3)grid [i-1,	j+1]);//-Vector3.one;
				v3 [1] = o.transform.InverseTransformPoint((Vector3)grid [i, 	j]);//-Vector3.one;
				v3 [2] = o.transform.InverseTransformPoint((Vector3)grid [i,	j+1]);//-Vector3.one;
				v3 [3] = o.transform.InverseTransformPoint((Vector3)grid [i-1,	j]);//-Vector3.one;
				o.GetComponent<MeshFilter> ().mesh.vertices = v3;
				o.GetComponent<MeshFilter> ().mesh.triangles = new int[] {0,1,2,1,0,3};
			}
		}
	}

	public void hit(bool c, Collider2D col = null){
		if (c) {
			Debug.Log ("hit");
			gameover = true;
		} else {
			//hit a poly collider, change its color??
			col.GetComponent<MeshRenderer>().material = m;
		}
	}
}
