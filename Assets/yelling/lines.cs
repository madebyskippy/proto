using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lines : MonoBehaviour {

	LineRenderer lr;

	Vector3[] original;
	Vector3[] target;
	Vector3[] verts;

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(){
		lr = GetComponent<LineRenderer> ();
		original = new Vector3[1];
		verts = new Vector3[1];
		target = new Vector3[1];
	}

	public void setV(Vector3[] ver){
		original = new Vector3[ver.Length + 1];
		verts = new Vector3[original.Length];
		target = new Vector3[original.Length];
		for (int i = 0; i < original.Length-1; i++) {
			Vector3 v = ver [i];
			v.x -= 3f;
			v.y -= 15f;
			v *= 0.3f;
			v.z = 2f;
			original [i] = v;
			verts [i] = v;
			target [i] = v;
		}
		original [original.Length - 1] = original [0];
		verts [verts.Length - 1] = verts [0];
		target [target.Length - 1] = target [0];
		lr .SetPositions (verts);
	}

	public void scale(float s){
		for (int i = 0; i < original.Length - 1; i++) {
			original [i] *= s;
			verts [i] *= s;
			target [i] *= s;
			original [i].z = 2f;
			verts [i].z = 2f;
			target [i].z = 2f;
		}
		original [original.Length - 1] = original [0];
		verts [original.Length - 1] = verts [0];
		target [original.Length - 1] = target [0];

		lr.SetPositions(original);
	}

	public void settarget(){
		for (int i = 0; i < target.Length; i++) {
			target [i] = verts [i];
		}
	}

	public void jiggle(){
		float magnitude = 0.01f;
		float threshhold = 0.075f;

		for (int i = 0; i < verts.Length-1; i++) {

			if (verts [i].x - target [i].x > threshhold) {
				verts[i].x += Random.Range (-1f, 0f) * magnitude;
			} else if (original [i].x - verts [i].x > threshhold) {
				verts[i].x += Random.Range (0f, 1f) * magnitude;
			} else {
				verts[i].x += Random.Range (-1f, 1f) * magnitude;
			}

			if (verts [i].y - target [i].y > threshhold) {
				verts[i].y += Random.Range (-1f, 0f) * magnitude;
			} else if (original [i].y - verts [i].y > threshhold) {
				verts[i].y += Random.Range (0f, 1f) * magnitude;
			} else {
				verts[i].y += Random.Range (-1f, 1f) * magnitude;
			}
		}
		verts [verts.Length - 1] = verts [0];

		lr.SetPositions(verts);
	}
//
	public void warp(){
		float magnitude = 0.01f;
		for (int i = 0; i < verts.Length-1; i++) {
			verts[i].x += Random.Range (-1f, 1f) * magnitude;
			verts[i].y += Random.Range (-1f, 1f) * magnitude;
		}
		verts [verts.Length - 1] = verts [0];

		lr.SetPositions(verts);
	}

	public void pop(){
		float magnitude = 0.01f;
		float probability = 0.01f;
		float s = 1f;
		for (int i = 0; i < verts.Length-1; i++) {
			if (Random.Range (0f, 1f) < probability) {
				s = 5f;
			} 

			verts [i].x += Random.Range (-1f, 1f) * magnitude * s;
			verts [i].y += Random.Range (-1f, 1f) * magnitude* s;
		}
		verts [verts.Length - 1] = verts [0];

		lr.SetPositions(verts);
	}

	public int getvlength(){
		return verts.Length;
	}
}
