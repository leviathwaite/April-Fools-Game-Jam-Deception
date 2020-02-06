using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]

public class PlayerRBController : MonoBehaviour {

	// control threshold
	public float inputDeadzone = 0.1f;
	// moving speed
	public float movementVel = 12;
	// turning speed
	public float rotateVel = 100;

	public float jumpForce = 2;

	Quaternion targetRotation;
	Rigidbody rBody;

	// x = horizontal, y = veritcal, 
	Vector2 input;

	public Quaternion TargetRotation{
		get {return targetRotation;}
	}

	// Use this for initialization
	void Start () {
		// target rotation starts at current rotation
		targetRotation = transform.rotation;
		// required rb
		rBody = GetComponent<Rigidbody>();

		input = new Vector2();
	}

	void GetInput(){
		// Horizontal and Vertical exist by default in the InputManager
		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		Turn();
	}

	void FixedUpdate () {
		Run();
	}

	void Run(){
		if(Mathf.Abs(input.y) > inputDeadzone){
			// move
			rBody.velocity = transform.forward * input.y * movementVel;
		}else{
			// zero velocity
			rBody.velocity = Vector3.zero;
		}
	}

	void Turn(){
		if(Mathf.Abs(input.x) > inputDeadzone){
			targetRotation *= Quaternion.AngleAxis(rotateVel * input.x * Time.deltaTime, Vector3.up);
		}
		transform.rotation = targetRotation;
	}
}
