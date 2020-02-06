using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// follow target/player for a top down game

public class TopDownCameraFollow : MonoBehaviour {

	// use to offset the camera rotation
	public Vector3 rotationOffset;
	// use to offset the camera rotation
	public Vector3 positionOffset;

	// smoothing variables
	public float movementSmoothing = 1;
	public float rotationSmoothing = 0.09f;

	public float rotationSpeed = 5;

	// reference for camera target
	private Transform target;

	// Use this for initialization
	void Start () {
		// assign player as target
	// SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
		SetTarget(GameObject.FindGameObjectWithTag("CameraNode").transform);
		// average mouse position and player position to use as target
		// TODO or cheat and just offset in the direction player is facing
	}

	// Used to set target, could be used to focus on enemy that kills player
	public void SetTarget(Transform t){
		if(t != null){
			target = t;
		}else{
			Debug.Log("Transform NULL");
			// TODO should I assign something if it's null? Player?
		}
		
	}
	
	
	void LateUpdate () {
		// moving
		MoveToTarget();
		// rotating
		LookAtTarget();
		// using player camera node
		// UseCameraNode();

		
	}

	void MoveToTarget(){
		Vector3 destination = target.rotation * positionOffset;
		destination += target.position;
		// transform.position = destination;
		transform.position = Vector3.Lerp(transform.position, destination, movementSmoothing * Time.deltaTime);
	}

	void UseCameraNode(){
		// Spins like crazy
		transform.LookAt(target.transform);
	}

	void LookAtTarget(){
		// float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotationSpeed, rotationSmoothing);
		// transform.rotation = Quaternion.Euler(xTilt, eulerYAngle, 0);
		
		// float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotationSpeed, rotationSmoothing);
		// transform.rotation = Quaternion.Euler(rotationOffset.x, rotationOffset.y - transform.eulerAngles.y, rotationOffset.z);
		
		/*
		var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * damping);
		*/

		// look the same direction as player
		// Quaternion targetsView = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * rotationSmoothing);
		
		//  trying to get lookat as a quaternion
		// Quaternion lookAtPlayer = transform.LookAt(target.position);

		// look at player, no lerp. Dizzying
		// transform.LookAt(target.position);

		
		// target direction
		Vector3 targetDirection = (transform.position - target.position).normalized;
		// target rotation with offsets
		Quaternion adjustedRotation = Quaternion.Euler(
			targetDirection.x + rotationOffset.x,
			targetDirection.y + rotationOffset.y,
			targetDirection.z + rotationOffset.z);
		// lerp towards adjusted rotation
		transform.rotation = Quaternion.RotateTowards(transform.rotation, adjustedRotation, Time.time * rotationSmoothing);
		
	}
}
