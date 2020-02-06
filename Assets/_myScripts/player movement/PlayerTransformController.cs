using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformController : MonoBehaviour {

	// control threshold
	public float inputDeadzone = 0.1f;
	// moving speed
	public float movementVel = 0.25f;
	// turning speed
	public float rotateVel = 100;

	Quaternion targetRotation;

	// x = horizontal, y = veritcal, 
	Vector2 input;

	public Quaternion TargetRotation{
		get {return targetRotation;}
	}

	// Use this for initialization
	void Start () {
		// target rotation starts at current rotation
		targetRotation = transform.rotation;

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
			transform.position += transform.forward * input.y * movementVel;
		}else{
			// zero velocity
			transform.Translate(Vector3.zero);
			 
		}
	}

	void Turn(){
		if(Mathf.Abs(input.x) > inputDeadzone){
			// use this if you want to modify the transform directly, apply below
			// targetRotation *= Quaternion.AngleAxis(rotateVel * input.x * Time.deltaTime, Vector3.up);
			transform.Rotate(0, rotateVel * input.x * Time.deltaTime, 0);
		}
		// apply directly to transform
		// transform.rotation = targetRotation;
	}
}
