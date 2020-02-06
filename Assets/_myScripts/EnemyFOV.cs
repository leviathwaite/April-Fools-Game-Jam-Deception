using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Script requires the GameObject to have a Sphere component
// TODO switch to circle/shpere cast
[RequireComponent(typeof(SphereCollider))]
public class EnemyFOV : MonoBehaviour {
	// TODO organize variables

	// TODO move somewhere else
	NavMeshAgent agent;

	public float maxDistance = 5;
	public float viewAngle = 45;

	public float rayLength = 5;
	public float rayLifeTimer = 5;

	// recentlySawPlayer Variables
	private bool recentlySawPlayer;
	// counter
	public float recentlySawTimer;
	// amount of time to wait
	public float recentlySawTime = 5;

		// reference variable for script on player
	private PlayerStatus playerStatus;
	

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		recentlySawPlayer = false;
		playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
		DebugDrawViewAngle();
		// Timer for recentlySawPlayer
		if(recentlySawPlayer){
			// timer finished
			if(recentlySawTimer <= 0){
				recentlySawPlayer = false;
				
			}
			// timer continues
			else{
				recentlySawTimer  -= Time.deltaTime;
			}
		}
	}

	void DebugDrawViewAngle(){
		// Length is not right but good enough for what I need
		// right side
		Vector3 direction = (Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward).normalized;
		direction = (direction * rayLength);
		// draw
		Debug.DrawRay(transform.position, direction, Color.red, rayLifeTimer);
		// left side
		direction = (Quaternion.AngleAxis(-viewAngle, transform.up) * transform.forward).normalized;
		direction = (direction * rayLength);
		// draw
		Debug.DrawRay(transform.position, direction, Color.red, rayLifeTimer);
	}

	// casts a ray at a target
	// hoping it will show line of site
	// TODO what to do after hit? return true?
	bool CastRay(Transform target){
		// Vector3 forward = transform.forward * rayLength;
		Vector3 forward = (target.position - transform.position).normalized;
		forward *= rayLength;

		Debug.DrawRay(transform.position, forward, Color.blue, rayLifeTimer);

		RaycastHit hit;
	
		Ray ray = new Ray(transform.position, forward);

		if(Physics.Raycast(ray, out hit, rayLength)){
			// player for now
			if(hit.collider.CompareTag("Player")){
				Debug.Log("Hit the player");
				
				SawPlayer();
				return true;
			}else{
				return false;
			}
		}
		return false;
	}

	void SawPlayer(){
		// Start/Setup timer
		recentlySawPlayer = true;
		recentlySawTimer = recentlySawTime;
		// Tell player he was hit
		playerStatus.Spotted();

		// TODO tell parent or animator of enemy

		// TODO temp, to test move towards player
		agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

	}

	// this uses required sphere collider, set as trigger
	void OnTriggerEnter(Collider other) {
        Debug.Log ("Trigger Entered");

		if(!recentlySawPlayer){
			if(other.CompareTag("Player")){
				if(InFOV(other.transform)){
					// TODO calculate angle before casting ray
					CastRay(other.transform);
				}
				
			}
		}
		
	}

	void OnTriggerStay(Collider other) {
        // Debug.Log ("Trigger Stayed or whatever...");

		if(!recentlySawPlayer){
			if(other.CompareTag("Player")){
				if(InFOV(other.transform)){
					// TODO calculate angle before casting ray
					CastRay(other.transform);
				}
				
			}
		}
		
	}

	// uses viewAngle to check angle difference, returns true if in FOV
	bool InFOV(Transform target){
		// get difference between positions
		Vector3 delta = transform.position - target.transform.position;
		// Would you look at it?
		Quaternion look = Quaternion.LookRotation(delta);
		// get angle on axis
		// float vertical = look.eulerAngles.x; // don't need for this case
		float horizontal = look.eulerAngles.y;
		// Debug.Log("horizontal: " + horizontal);
		// adjust angle(off by 180), absolute value
		float angle = Mathf.Abs((horizontal - 180) % 360);
		// Debug.Log("angle: " + angle);
		// compare angle with viewAngle
		if(viewAngle > angle){
			return true;
		}

		return false;
	}

}
