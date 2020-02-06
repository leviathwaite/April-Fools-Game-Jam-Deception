using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to make game object with collider or trigger hurt the player by the amount set
// player must have a PlayerHealth script
[RequireComponent(typeof(Collider))]

public class HurtPlayer : MonoBehaviour {
	// ref to player
	GameObject player;
	// amount of damage caused
	public int damage = 5;
	// one time attack, destroy self
	public bool oneHitWonder = false;
	// ref to health bar
	PlayerHealth playerHealth;
	// time delay for destroy
	private float destroyDelay = 0.25f;


	// Use this for initialization
	void Start () {
		//setup ref to player
		player = GameObject.FindGameObjectWithTag("Player");
		// setup ref to playerHealth
		playerHealth = player.GetComponent<PlayerHealth>();
		if(playerHealth == null){
			Debug.LogError("HurtPLayer, player does not have PlayerHealth attached");
		}
	}
	
	/* COLLISIONS OR TRIGGERS */
	// TODO should I have both or add ENUM to toggle between them...
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Player")){
			// add to player inventory
			HitPlayer();	
		}
	}	

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")){
			// add to player inventory
			HitPlayer();
		}
	}

	// hurt the player, used by enemy ai(doesnt get close enough to trigger collision)
	public void HitPlayer(){
		if(oneHitWonder){
				MyOnDestroy();
			}
		playerHealth.TakeHealth(damage);
	}

	void MyOnDestroy(){
		gameObject.SetActive(false);
		// TODO add effects
		Destroy(gameObject, destroyDelay);
	}

}
