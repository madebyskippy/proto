using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.UI;

//mouse look taken from http://answers.unity3d.com/questions/29741/mouse-look-script.html

public class mouselookfps : MonoBehaviour
{

	[SerializeField] private Camera cam;
	[SerializeField] private GameObject player;
	[SerializeField] public Sprite[] masks;
	[SerializeField] public GameObject window;
	private int currentCam;

	//for generating peeps
	public GameObject peoplePrefab;
	private int interval = 1; //in seconds
	private float timeLeft;
	private int peopleCount;

	//for movement
	private float speed = 0.15f;
	private int moveCullingMask;
	[SerializeField] GameObject anim;

	//for mouselook
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion[] originalRotation;

	//for shoot
	public GameObject crosshair;
	public GameObject voice;
	public Sprite point;
	public Sprite shh;

	//for start/end ui stuffos
	public GameObject start;
	public GameObject endWin;
	public GameObject endLose;
	public GameObject whiteout;
	private string mode;

	void Start ()
	{
		// Make the rigid body not change rotation
//		if (rigidbody)
//			rigidbody.freezeRotation = true;
		mode = "start";
		start.SetActive(true);
		endWin.SetActive (false);
		endLose.SetActive (false);
		whiteout.SetActive (false);
		currentCam=0;
		originalRotation = new Quaternion[3];
		originalRotation[0] = cam.transform.localRotation;
		moveCullingMask = cam.cullingMask;
		window.GetComponent<Image> ().sprite = masks [currentCam];
		crosshair.SetActive (false);
		timeLeft = interval;
		peopleCount = 0;
		placePeople ();
	}

	void Update ()
	{
		if (currentCam == 0 && (mode == "game" || mode == "win" || mode == "start")) {
			//walk
			player.transform.position += new Vector3 (Input.GetAxis ("Horizontal")*speed, 0f, Input.GetAxis("Vertical")*speed);

			//wraparound
			if (player.transform.position.z < -25) {
				player.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,22f);
			} if (player.transform.position.z > 22) {
				player.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-25);
			} if (player.transform.position.x > 24) {
				player.transform.position = new Vector3(-24f,player.transform.position.y,player.transform.position.z);
			} if (player.transform.position.x < -24) {
				player.transform.position = new Vector3(24f,player.transform.position.y,player.transform.position.z);
			}

			//animations
			if (Input.GetAxis ("Vertical") ==0 && Input.GetAxis("Horizontal")==0){
				anim.GetComponent<Animator> ().Play ("walkstill");
			} else if (Input.GetAxis ("Vertical") > 0) {
				anim.GetComponent<Animator> ().Play ("walkfor1");
			} else if (Input.GetAxis ("Vertical") < 0) {
				anim.GetComponent<Animator> ().Play ("walkback");
			} else if (Input.GetAxis ("Horizontal") > 0) {
				anim.GetComponent<Animator> ().Play ("walkright");
			} else if (Input.GetAxis ("Horizontal") < 0) {
				anim.GetComponent<Animator> ().Play ("walkleft");
			}
		}
		if (currentCam == 1 && (mode == "game" || mode == "win")) {
			//look
			if (axes == RotationAxes.MouseXAndY) {
				// Read the mouse input axis
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationX = ClampAngle (rotationX, minimumX, maximumX);
				rotationY = ClampAngle (rotationY, minimumY, maximumY);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
				Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
				//			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
				cam.transform.localRotation = originalRotation [0] * xQuaternion * yQuaternion;
			} else if (axes == RotationAxes.MouseX) {
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
				rotationX = ClampAngle (rotationX, minimumX, maximumX);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
				//			transform.localRotation = originalRotation * xQuaternion;
				cam.transform.localRotation = originalRotation [0] * xQuaternion;
			} else {
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = ClampAngle (rotationY, minimumY, maximumY);
				Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
				//			transform.localRotation = originalRotation * yQuaternion;
				cam.transform.localRotation = originalRotation [0] * yQuaternion;
			}
		}
		if (currentCam == 2 && (mode == "game" || mode == "win")) {
			//shoot
			crosshair.transform.position = Input.mousePosition;

			if (Input.GetMouseButtonDown (0)) {
				//left click, shoot
				crosshair.GetComponent<Image> ().sprite = shh;
				voice.GetComponent<Animator> ().Play ("shhh", -1, 0f);
				Invoke ("backToPoint", 0.25f);
				RaycastHit hit;
				Ray ray = cam.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
//					hit.transform.localScale = new Vector3 (2f, 2f, 2f);
					if (hit.transform.gameObject.tag=="Respawn")
						hitObject(hit.transform.gameObject);
				}
//			} if (Input.GetMouseButtonUp (0)) {
//				crosshair.GetComponent<Image> ().sprite = point;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (mode == "start") {
				mode = "game";
				start.SetActive(false);
				timeLeft = interval;
			}
//			cams [currentCam].enabled = false;
			currentCam = (currentCam + 1) % 3;
//			cams [currentCam].enabled = true;
			cam.rect = new Rect (0.33f * currentCam, 0, 0.33f, 1f);
			window.GetComponent<Image> ().sprite = masks [currentCam];
			if (currentCam == 0) {
				//changing to walk
				cam.cullingMask = moveCullingMask;
				crosshair.SetActive (false);
			} else if (currentCam == 1) {
				cam.cullingMask = 1;
				cam.backgroundColor = new Color (206f/255f,220f/255f,1,1);
			} else if (currentCam == 2) {
				cam.backgroundColor = new Color (194f/255f,241f/255f,167f/255f,1);
				crosshair.SetActive (true);
			}
		}

		timeLeft -= Time.deltaTime;
		if (timeLeft < 0 && mode == "game") {
			timeLeft = interval;
			placePerson ();
		}
		if (peopleCount > 70) {
			//endgame lose
			endLose.SetActive (true);
			whiteout.SetActive (true);
			mode = "lose";
		}
		if (peopleCount <= 0) {
			//endgame win
			endWin.SetActive (true);
			mode = "win";
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("fps");
		}
	}
	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}

	void placePeople(){
		for (int i = 0; i < 10; i++) {
			GameObject temp = Instantiate (peoplePrefab);
			temp.transform.position = new Vector3(Random.Range(-25f,25f),0f,Random.Range(-25f,25f));
			peopleCount++;
		}
	}

	void placePerson(){
		GameObject temp = Instantiate (peoplePrefab);
		temp.transform.position = new Vector3(Random.Range(-25f,25f),0f,Random.Range(-25f,25f));
		peopleCount++;
	}

	void backToPoint(){
		crosshair.GetComponent<Image> ().sprite = point;
	}

	void hitObject(GameObject h){
		peopleCount--;
		string state = h.transform.parent.GetComponent<billboard> ().version;;
		h.transform.parent.GetComponent<Animator> ().Play (state+"s");
		Destroy (h);
	}
}
