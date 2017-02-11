using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.UI;

//taken from http://answers.unity3d.com/questions/29741/mouse-look-script.html

public class mouselook : MonoBehaviour
{

	[SerializeField] private Camera cam;
	[SerializeField] private GameObject player;
	[SerializeField] public Sprite[] masks;
	[SerializeField] public GameObject window;
	private int currentCam;

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


	void Start ()
	{
		// Make the rigid body not change rotation
//		if (rigidbody)
//			rigidbody.freezeRotation = true;
		currentCam=0;
		originalRotation = new Quaternion[3];
		originalRotation[0] = cam.transform.localRotation;
		moveCullingMask = cam.cullingMask;
		window.GetComponent<Image> ().sprite = masks [currentCam];
	}

	void Update ()
	{
		if (currentCam == 0) {
			//walk
			player.transform.position += new Vector3 (Input.GetAxis ("Horizontal")*speed, 0f, Input.GetAxis("Vertical")*speed);

			if (Input.GetAxis ("Vertical") ==0 && Input.GetAxis("Horizontal")==0){
				anim.GetComponent<Animator> ().Play ("walkstill");
			} else if (Input.GetAxis ("Vertical") > 0) {
				anim.GetComponent<Animator> ().Play ("walkfor1");
			} else if (Input.GetAxis ("Vertical") < 0) {
				anim.GetComponent<Animator> ().Play ("walkback");
			}  else if (Input.GetAxis ("Horizontal") > 0) {
				anim.GetComponent<Animator> ().Play ("walkright");
			} else if (Input.GetAxis ("Horizontal") < 0) {
				anim.GetComponent<Animator> ().Play ("walkleft");
			}
		}
		if (currentCam == 1) {
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
		if (currentCam == 2) {
			//shoot
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
//			cams [currentCam].enabled = false;
			currentCam = (currentCam + 1) % 3;
//			cams [currentCam].enabled = true;
			cam.rect = new Rect (0.33f * currentCam, 0, 0.33f, 1f);
			window.GetComponent<Image> ().sprite = masks [currentCam];
			if (currentCam == 0) {
				//changing to walk
				cam.cullingMask = moveCullingMask;
			} else if (currentCam == 1) {
				cam.cullingMask = 1;
			}
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
}
