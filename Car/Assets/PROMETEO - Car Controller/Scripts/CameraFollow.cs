using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform carTransform;

	[Range(1, 100)]
	public float followSpeed = 15;

	[Range(1, 100)]
	public float lookSpeed = 75;
	
	[Range(1, 100)]
	public float distanceBehindCar = 33;

	[Range(1, 100)]
	public float heightAboveCar = 9;
	

	void FixedUpdate()
	{
		// Ensure the camera stays behind the car's forward direction with adjustable distance and height
		Vector3 _targetPos = carTransform.position - carTransform.forward * distanceBehindCar + carTransform.up * heightAboveCar;
    
		// Move the camera smoothly to the target position
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);

		// Rotate the camera to always look at the car from behind
		Quaternion _rot = Quaternion.LookRotation(carTransform.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}
}
