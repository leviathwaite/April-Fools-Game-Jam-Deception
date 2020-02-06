using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour {


	public float inputDeadzone = 0.1f;
	public float walkSpeed = 50;
	public float runSpeed = 100;
	public float turnSpeed = 10;
	public float strafeSpeed = 5;
	public bool running = false;
	// min distance between player and mouse
	// jitters too much, maybe try using cameraNode
	public float minDistance = 3;

	public LayerMask groundLayerMask;

	private Vector3 input;
	private Vector3 movement;
	private Quaternion targetRotation;
	private Rigidbody rb;
	private Animator anim;

	// used for predicting future position
	private Vector3 lastPosition; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		targetRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		Turn();
		UpdateAnimator();
	}

	void GetInput(){
		// get input
		input = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical")
		);

		// toggle running
		
		if(Input.GetButton("Fire3")){
			running = !running;
			Debug.Log("Running Toggled");
		}

	}

	void FixedUpdate(){
		MoveBasedOnLocal();
		// MoveBasedOnWorld();
	}

	// use L shift to toggle runnning
	// always strafe
	void MoveBasedOnWorld(){
		float movementSpeed;
		if(running){
			movementSpeed = runSpeed;
		}else{
			movementSpeed = walkSpeed;
		}

		 // Set the movement vector based on the axis input.
        movement.Set (input.x, 0f, input.z);
        
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * movementSpeed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        rb.MovePosition (transform.position + movement);
	}

	void MoveBasedOnLocal(){

		lastPosition = transform.position;

		float movementSpeed;
		if(running){
			movementSpeed = runSpeed;
		}else{
			movementSpeed = walkSpeed;
		}

		 // Set the movement vector based on the axis input.
        movement.Set (input.x, 0f, input.z);
        
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * movementSpeed * Time.deltaTime;
		// convert to local space
		movement = transform.TransformDirection(movement);

        // Move the player to it's current position plus the movement.
        rb.MovePosition (transform.position + movement);

	}

	// Make player face mouse position
	void Turn(){
		// get mouse point
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;

		// if (Physics.Raycast(cameraRay.direction, Vector3.down, out hit, Mathf.Infinity, groundLayerMask)){
		if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, groundLayerMask))
		{
			// get point to look at
			Vector3 pointToLookAt = hit.point - transform.position;
			 // So we don't look at the floor
            pointToLookAt.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation (pointToLookAt);

            // Set the player's rotation to this new rotation.
            rb.MoveRotation (newRotation);
			
		}
		
		
	}

	// TODO needs work
	// Given position and speed calculate where player will be in the future,
	// used to head player of at the pass 
	public Vector3 GetFuturePosition(Vector3 v, float speed){

		// distance between two positions
		float distance = Vector3.Distance(transform.position, v);
		// time it would take to get to player
		// float travelTime = distance / speed; seems like a better choice
	    float travelTime = distance / speed; // but this feels more accurate...

		// which way the player is moving
		Vector3 playerHeading = (transform.position - lastPosition); // .normalized;
		// player velocity
		float playerSpeed = ((transform.position - lastPosition).magnitude / Time.fixedDeltaTime);

		// current position + where the player is heading(at current speed * the time of intercept)
		return transform.position + ((playerHeading * playerSpeed) * travelTime);
	}

	public Vector3 GetLastPosition(){
		return lastPosition;
	}

	// used for enemies to predict movement
	public Vector3 GetPosition(){
		return transform.position;
	}

	public Vector3 GetDirection(){
		return transform.position - lastPosition;
	}

	public float GetVelocity(){
		// (transform.position - oldPosition).magnitude / Time.deltaTime or fixedDeltaTime
		return ((transform.position - lastPosition).magnitude / Time.fixedDeltaTime);
	}

	void UpdateAnimator(){

	}
}
