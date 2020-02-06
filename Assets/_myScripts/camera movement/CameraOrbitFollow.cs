using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://youtu.be/BBS2nIKzmbw?list=PL4CCSwmU04MjDqjY_gdroxHe85Ex5Q6Dy

public class CameraOrbitFollow : MonoBehaviour {

	public Transform target;
	public float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
	public float xTilt = 10;

	Vector3 destination = Vector3.zero;
	PlayerRBController charController;
	float rotateVel = 0;

	// Use this for initialization
	void Start () {
		// SetCameraTarget(target);
		SetCameraTarget(GameObject.FindGameObjectWithTag("Player").transform);
	}
	
	public void SetCameraTarget(Transform t){
		target = t;

		if(target != null){
			if(target.GetComponent<PlayerRBController>()){
				charController = target.GetComponent<PlayerRBController>();
			}else if(target.GetComponent<PlayerMovementWithJump>()){
				charController = target.GetComponent<PlayerRBController>();
			}
			else{
				Debug.Log("The camera's target need a PlayerRBController or PlayerMovementWithJump");
			}
		}else{
			Debug.LogError("Your camera needs a target");
		}
	}

	void LateUpdate () {
		// moving
		MoveToTarget();
		// rotating
		LookAtTarget();
	}

	void MoveToTarget(){
		destination = charController.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget(){
		float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler(xTilt, eulerYAngle, 0);
		// transform.rotation = Quaternion.Euler(xTilt, eulerYAngle, 0);"﻿
	}
}
