using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyController : MonoBehaviour {
	// FIXME TODO GameManager needs to be singleton

	 // TODO move states to AnimatorController
	 private Animator anim;

	 /* https://software.intel.com/en-us/articles/designing-artificial-intelligence-for-games-part-1 */

	 /*
	 Idle. In this state, the entity is passively standing around or walking along a set path. 
	 Perceptions are low. Player sounds are not often checked for. 
	 Only if this entity is attacked or “sees” a player directly 
	 in front of it will its state change to a higher level of awareness.

	 * Alert/Hunting if player is seen

	 * Aware/Searching/Patrol after amount of time Idle

	 * Dead if health = 0

	 Aware/Searching/Patrol. This entity is actively searching for intruders. 
	 It checks often for the sounds of the player and sees farther and
	 wider than an idle entity. This entity will move to the Intrigued state
	 if it notices something out of place (something to check for), 
	 such as open doors, unconscious bodies, or spent bullet casings.

	 * Intrigued/Suspicous if finds signs of player

	 * Dead if health = 0
	
	Intrigued/Suspicous. This entity is aware that something is up. To demonstrate this behavior, 
	the entity will abandon its normal post or path and move to areas of interest, 
	such as the aforementioned open doors or bodies. If a player is seen, 
	the entity goes to the Alert state.
	
	Alert/Hunting. In this state, the entity has become aware of the player and will 
	go through the actions of hunting down the player: moving into range of attack, 
	alerting fellow guards, sounding alarms, and finding cover. 
	When the entity is within range of the enemy, it switches to the Aggressive state.
	
	Aggressive/Attack. This is the state where the enemy has engaged in combat with the player. 
	The entity attacks the player when it can and seeks cover between rounds of attack 
	(based on attack cool-downs or reloading). The entity only leaves this state if the enemy
	is killed (return to normal), if the enemy moves out of firing range 
	(go back to the Alert stage), or if the entity dies (go to the Dead state). 
	If the entity becomes low on health, it may switch to the Fleeing state, 
	depending on the courage of the specific entity.
	
	Fleeing/Evade. In this state, the entity tries to run from combat. Depending on the game, 
	there may be a secondary goal of finding health or leaving the play area. 
	When the entity finds health, it may return to the Alert state and resume combat. 
	An entity that “leaves” is merely deleted.
	
	Dead. In some games, the state of death may not be completely idle. 
	Death or dying can have the entity “cry out,” alerting nearby entities, 
	or go into a knocked-out state, where it can later be revived by a medic 
	(and returned to a state of Alert).
	*/

	 int idleHash;
	 int walkHash;
	 // int attackHash;
	 // Distance to player to attack
	 public float attackDistance = 2;
	 // Too far from player, move Towards or patrol
	 public float maxDistance = 10;

	 private GameObject player;

	LootSpawner lootSpawner;

	private float destroyDelay = 0.5f;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		lootSpawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LootSpawner>();
	
		SetupAnimator();
	}

	void SetupAnimator(){
		anim = GetComponent<Animator>();
	}

	void SetupStateHashes(){
		// TODO get states from animator and store in list or array, maybe even setup enum
		idleHash = Animator.StringToHash("Idle");
		walkHash = Animator.StringToHash("Walk");
		// attackHash = Animator.StringToHash("Attack");

	}

	void Update(){
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if( distanceToPlayer <= attackDistance){
			// Attack
		}else if(distanceToPlayer > maxDistance){
			// Go towards player or Patrol

		}
	}

	void Attack(){
		// TODO if collides with player take health
	}

	void Move(){
		// move towards target, player or patrol
		
	}

	/* 
	void OnCollisionEnter(Collision collision) {
		Debug.Log("Enemy Has been hit, ");
		Debug.Log(collision.gameObject.tag);
		if(collision.gameObject.CompareTag("PlayerBullet")){

			MyOnDestroy();
			
		}
	}
	*/



	void OnTriggerEnter(Collider other) {
		Debug.Log("Easy Enemy Trigger Entered, " + other.tag);
		if(other.CompareTag("PlayerBullet")){
			MyOnDestroy();
		}
}

	void MyOnDestroy(){
		// make invisible
		gameObject.SetActive(false);
		// drop loot
		lootSpawner.spawnLoot(transform);

		// TODO add effects
		Destroy(gameObject, destroyDelay);
	}
}
