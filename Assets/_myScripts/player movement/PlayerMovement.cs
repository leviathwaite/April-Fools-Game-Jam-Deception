using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    /* 
 public float speed = 2.0f;
 
    Vector3 movement;
    Vector3 rotating;
    Rigidbody playerRigidBody;
    bool isMoving = false;
    int floorMask;
    float camRayLength = 100.0f;
    public bool isEnabled = true;
 
    // Use this for initialization
    void Start () {
        GameObject player = GameObject.FindGameObjectWithTag ("Player");
    }
     
    // Update is called once per frame
    void Update () {
     
    }
 
    void Awake() {
        playerRigidBody = GetComponent<Rigidbody> ();
        floorMask = LayerMask.GetMask ("Ground");
    }
     
    void FixedUpdate (){
        if (isEnabled == false)
            return;
        // get button pressed by player
        float h = CrossPlatformInputManager.GetAxisRaw ("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw ("Vertical");
         
        Move (h, v);
         
         
        if (h !=0 || v != 0) {
            isMoving = true;
        } else {
            isMoving = false;
        }
        Turning ();
    }
     
     
    void Move( float h, float v){
        movement.Set (v, 0f, -h);
         
        movement = movement.normalized * Time.fixedDeltaTime * speed;
         
        playerRigidBody.MovePosition (transform.position + movement);
         
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
            playerRigidBody.MoveRotation(newRotation);
             
        }
        #else
        Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse Y"), 0f, CrossPlatformInputManager.GetAxisRaw("Mouse X"));
        if(turnDir != Vector3.zero){
            Vector3 playerToMouse = (transform.position - turnDir) - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation); 
        }
        #endif
    }
     
     
    public void DisableMovement(){
        isEnabled = false;
    }
    */
}
