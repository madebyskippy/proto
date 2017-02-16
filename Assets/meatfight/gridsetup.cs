using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class gridsetup : MonoBehaviour {
	[SerializeField] GameObject gridPrefab;

	[SerializeField] private int rows = 16;
	[SerializeField] private int col = 9;

	// Update is called when something changes in the scene
	void Update () {
		if (this.transform.childCount < (rows*col))	{
			for (int i = 0; i < 16; i++) {
				for (int j = 0; j < 9; j++) {
					GameObject temp = Instantiate (gridPrefab);
					temp.transform.position = new Vector3 (i-7.57f, j-3.99f, 0f);
					temp.transform.parent = this.gameObject.transform;
				}
			}
		}
	}
}
