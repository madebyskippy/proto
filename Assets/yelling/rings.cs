using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rings : MonoBehaviour {

	[SerializeField] GameObject ring;

	public int max = 5;
	public float size = 10f;
	public float pause = 0.5f;

	private int max_temp;
	private float size_temp;
	private float pause_temp;

	private float interval = 0.02f;
	private int count;

	private List<GameObject> objs;

	// Use this for initialization
	void Start () {
		max_temp = max;
		size_temp = size;
		pause_temp = pause;
		objs = new List<GameObject> ();
		count = 0;
//		begin ();

		for (int i = 0; i < max; i++) {
			GameObject temp = Instantiate (ring);
			temp.transform.parent = this.transform;
			temp.transform.position = transform.position;
			temp.transform.localScale = Vector3.zero;
			objs.Add (temp);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void begin(){
		max = max_temp;
		size = size_temp;
		pause = pause_temp;
		count = 0;

		for (int i = 0; i < max; i++) {
			count++;
			createring (i);
		}

	}

	void createring(int i){

		GameObject obj = objs [i];
		GameObject child = objs [i].transform.GetChild (0).gameObject;

		float ratio = (count * 1f) / (max * 1f);

		float s = (1 - ratio + 0.5f) * size;
		obj.transform.localScale = Vector3.one * Random.Range(s*0.75f, s);

		child.transform.localScale = Vector3.one * Random.Range (0.7f, 0.95f);

		float scale = obj.transform.localScale.x;
		float scalei = child.transform.localScale.x;
		obj.transform.localScale = Vector3.zero;
//		temp.transform.DOScale (scale, interval * max);
		Sequence sq = DOTween.Sequence ();
		sq.Append (obj.transform.DOScale (scale, interval * max));
		sq.Append (child.transform.DOScale (1f, pause));
//		sq.Append (temp.transform.DOScale (0f, interval * max*5f));

		SpriteRenderer sr = obj.GetComponent<SpriteRenderer> ();
		sr.color = Random.ColorHSV (0f,1f,0f,0.25f,0.6f,0.7f);
//		sr.sortingOrder = count * 2;
		child.GetComponent<SpriteRenderer> ().sortingOrder = 0;//count * 2 + 1;
		Vector3 temp = obj.transform.position;
		temp.z = 20f - count * 2f;
		obj.transform.position = temp;
		temp = child.transform.position;
		temp.z = obj.transform.position.z-0.1f;
		child.transform.position = temp;

	}

	public void changeMax(float val){
		max_temp = (int)val;
	}
	public void changeSize(float val){
		size_temp = val;
	}
	public void changePause(float val){
		pause_temp = val;
	}
}
