using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CS_GridSetup : MonoBehaviour {

	[SerializeField] bool createGrids = false;

	[SerializeField] GameObject myGridPrefab;

	[SerializeField] int myColumns = 16;
	[SerializeField] int myRows = 9;

	[SerializeField] Vector2 myBottomLeft = new Vector2 (-8, -4);
	[SerializeField] Vector2 myTopRight = new Vector2 (8, 4);
	[SerializeField] float myPositionZ = 10;

	private List<GameObject> myGrids = new List<GameObject> ();


	// Update is called when something changes in the scene
	void Update () {
		if (createGrids == true) {
			createGrids = false;

			foreach (GameObject t_grid in myGrids) {
//				Destroy (t_grid);
				GameObject.DestroyImmediate (t_grid);
			}
			myGrids.Clear ();

			for (int i = 0; i < myRows; i++) {
				for (int j = 0; j < myColumns; j++) {
					Vector3 t_position = new Vector3 (
						                     (myTopRight.x - myBottomLeft.x) / (myColumns - 1) * j + myBottomLeft.x, 
						                     (myTopRight.y - myBottomLeft.y) / (myRows - 1) * i + myBottomLeft.y,
						                     myPositionZ);
					GameObject t_grid = Instantiate (myGridPrefab, t_position, Quaternion.identity);
					myGrids.Add (t_grid);
					t_grid.transform.SetParent (this.transform);
					t_grid.name = myGridPrefab.name + "(" + i + ")(" + j + ")";
				}
			}
		}
	}

	public List<GameObject> getgrid(){
		return myGrids;
	}
}
