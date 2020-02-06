using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPatientMovement : MonoBehaviour {

	[SerializeField]float speed;

	public LayerMask floorMask;
    float camRayLength = 100.0f;
	Rigidbody rigidbody;
	private Vector3 input;
	private Vector3 movement;

	// use to disable movement
	bool isEnabled = true;
	// use to animate
	bool isMoving;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		// get player input
		input.y = Input.GetAxis("Horizontal");
		input.x = -Input.GetAxis("Vertical");
	}

	void FixedUpdate(){
		// use to disable movement
		if (isEnabled == false)
            return;

		// move player
		 Move (input.x, input.y);
    
		 // check if player is moving for animation
        if (input.x !=0 || input.y != 0) {
            isMoving = true;
        } else {
            isMoving = false;
        }

		// turn the player
        Turning ();
	}
  void Move( float h, float v){
        movement.Set (v, 0f, -h);
         
        movement = movement.normalized * Time.fixedDeltaTime * speed;
         
        // rigidbody.MovePosition (transform.position + movement);
        transform.Translate(movement);
         
    }
 
    public void Turning(){
        //Find Mouse Position and check if in range
        //if in range, rotate character towards mouse
         
        #if !MOBILE_INPUT
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rigidbody.MoveRotation(newRotation);
             
        }
        #endif
    }
     
     
    public void DisableMovement(){
        isEnabled = false;
    }
}
