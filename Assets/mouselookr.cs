using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselookr : MonoBehaviour {

	[SerializeField] Camera[] cams;

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

	//freezing stuffos
	private float xClamp = 20f;
	private bool isclamped;

	void Start ()
	{
		originalRotation = new Quaternion[3];
		originalRotation[0] = cams[0].transform.localRotation;

		isclamped = false;
	}

	void Update ()
	{
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
//			cam.transform.localRotation = originalRotation [0] * xQuaternion * yQuaternion;
			changeAllCams(originalRotation[0] * yQuaternion * xQuaternion);
		} else if (axes == RotationAxes.MouseX) {
			rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			//			transform.localRotation = originalRotation * xQuaternion;
//			cam.transform.localRotation = originalRotation [0] * xQuaternion;
			changeAllCams(originalRotation[0] * xQuaternion);
		} else {
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			//			transform.localRotation = originalRotation * yQuaternion;
//			cam.transform.localRotation = originalRotation [0] * yQuaternion;
			changeAllCams(originalRotation[0] * yQuaternion);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
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

	void changeAllCams(Quaternion rot){
		for (int i = 0; i < cams.Length; i++) {
			cams [i].transform.localRotation = rot;
		}
	}
}
