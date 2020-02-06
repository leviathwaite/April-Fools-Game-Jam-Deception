using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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


// Require Animator 
public class SimpleEnemyFSMController : MonoBehaviour {

	// TODO seperate stats to scriptable object
	public float speed = 5;
	public float positionPredictionAccuracy = 30;

	// help ai catch player by cutting them off at the pass
	public bool smartChase = false;

	// ref to player
	GameObject player;

	// use to predict future position
	PlayerMovementTopDown playerMovementTopDown;

	HurtPlayer hurtPlayer;

	Animator anim;

	NavMeshAgent agent;

	/* FSM State hashes */
	int idleHash;
	int chaseHash;
	int evadeHash;
	int attackHash;
	int deadHash;
	

		//Define Enum
     public enum StatesEnum{IDLE, CHASE, EVADE, ATTACK, DEAD};
	
     
     // stores currentState
     public StatesEnum currentState;
	 public StatesEnum lastState;

	 private float destroyDelay = 0.5f;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		playerMovementTopDown = player.GetComponent<PlayerMovementTopDown>();
		hurtPlayer = GetComponent<HurtPlayer>();

		agent = GetComponent<NavMeshAgent>();

		SetupAnimator();
		SetupStateHashes();

		// starting state
		SetStartingState();
	}
	// get ref to animator
	void SetupAnimator(){
		anim = GetComponent<Animator>();
	}

	void SetupStateHashes(){
		// TODO get states from animator and store in list or array, maybe even setup enum
		idleHash = Animator.StringToHash("Idle");
		chaseHash = Animator.StringToHash("Chase");
		evadeHash = Animator.StringToHash("Evade");
		attackHash = Animator.StringToHash("Attack");
		deadHash = Animator.StringToHash("Dead");
		
	}

	void SetStartingState(){
		currentState = StatesEnum.IDLE;
		lastState = currentState;
	}
	
	// Update is called once per frame
	void Update () {
		// check curent state from anim
		SetState();
		// check if state has been changed
		if(currentState != lastState){
			lastState = currentState;
			// FIXME remove
			Debug.Log("Enemy State Changed: " + currentState.ToString());
			ReActToState();
		}

	}

	void UpdateAnimator(){
		// TODO redo everything
		anim.SetFloat("playerDistance", Vector3.Distance(transform.position, player.transform.position));
		
	}

	// find current state
	void SetState(){
			// get current state
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		// int stateInfo = anim.GetCurrentAnimatorStateInfo(0).
		// tagHash 0
		// nameHash changes for different states
		// idlehash = 2081823275
		// shortNameHash = 2081823275

		// debug
		//Debug.Log(stateInfo.shortNameHash);

		// handle chase state, wish I could use a switch statement
		if(stateInfo.shortNameHash == idleHash){
			currentState = StatesEnum.IDLE;
		}else if(stateInfo.shortNameHash == chaseHash){
			currentState = StatesEnum.CHASE;
		}else if(stateInfo.shortNameHash == evadeHash){
			currentState = StatesEnum.EVADE;
		}else if(stateInfo.shortNameHash == attackHash){
			currentState = StatesEnum.ATTACK;
		}else if(stateInfo.shortNameHash == deadHash){
			currentState = StatesEnum.DEAD;
		}	
	}

	void ReActToState(){
		if(currentState == StatesEnum.IDLE){
			Idle();
		}else if(currentState == StatesEnum.CHASE){
			Chase();
		}else if(currentState == StatesEnum.EVADE){
			Evade();
		}else if(currentState == StatesEnum.ATTACK){
			Attack();
		}else if(currentState == StatesEnum.DEAD){
			Dead();
		}
	}

	/* States */

	// probably do nothing or decide to patrol or search..
	void Idle(){

	}

	// go after player/target
	void Chase(){
		/*
		// try to find future position
		// position = position + velocity * T

		// adjust T by distance
		// T = distanceBetweenTargetAndPursuer / MAX_VELOCITY
		float distance = Vector3.Distance(transform.position, player.transform.position);
		// lerp clamps to range[0-1]
		float t = (distance / speed);
		// time it would take to get to target
		float travelTime = distance / speed;
		// where to aim, position + (velocity * (distance from enemy * enemy's speed))
		// TODO this is all kinds of wrong but works for my use
		targetVector.transform.position = player.transform.position + (playerMovementTopDown.GetDirection() * (distance * speed));
		// Bisecting Vectors with weight
		//when t = 0 player position, t = 1 target position
		Vector3 targetVectorPosition = Vector3.Lerp(player.transform.position, targetVector.transform.position, t);
		 */

		 if(smartChase){
			// adjust T by distance
			// T = distanceBetweenTargetAndPursuer / MAX_VELOCITY
			float distance = Vector3.Distance(transform.position, player.transform.position);
			// lerp clamps to range[0-1]
			float t = (distance / speed);
			// use distance to decide between following player or if long ways off, trying to predict position
			Vector3 targetVectorPosition = Vector3.Lerp(player.transform.position, playerMovementTopDown.GetFuturePosition(transform.position, speed), t);
			// set agent target
			agent.SetDestination(targetVectorPosition);
		 }else{
			agent.SetDestination(player.transform.position);

		 }

	}

	// run away from player/target
	void Evade(){
		/*
		float distance = Vector3.Distance(transform.position, player.transform.position);
		float t = distance / speed;
		*/

		// adjust T by distance
		// T = distanceBetweenTargetAndPursuer / MAX_VELOCITY
		float distance = Vector3.Distance(transform.position, player.transform.position);
		// lerp clamps to range[0-1]
		float t = (distance / speed);
		// use distance to decide between following player or if long ways off, trying to predict position
		Vector3 targetVectorPosition = Vector3.Lerp(-player.transform.position, -playerMovementTopDown.GetFuturePosition(transform.position, speed), t);
		// set agent target
		agent.SetDestination(targetVectorPosition);

		

		// Bisecting Vectors with weight
		// Vector3 targetVectorPosition = Vector3.Lerp(player.transform.position, playerMovementTopDown.FuturePosition(), t);
		
		// agent.SetDestination(targetVectorPosition);
	}

/* 
	// predict target's future location
	void Vector3 FuturePosition(){

		// direction and magnitude of movement
		Vector3 playerDirection = playerMovementTopDown.GetDirection();

		// distance, divide direction magnitude by time passed(roughly)
		float futureDistance = (playerDirection.magnitude)/Time.deltaTime;

		// future position
		return transform.position + (futureDirection.normalized * (futureDistance * enemytime));
	}
	*/

	// attack player/target
	// anim shoud only set this to state when player is within attack distance
	// TODO scrap anim and use this class for state control and seperate actions
	// into their own classes with self contained requirements..???
	void Attack(){
		// needs a timer between attacks
		hurtPlayer.HitPlayer();
	}

	// don't do much here
	void Dead(){
		
		MyOnDestroy();
	}

	// used to play effects when gameobject is destroyed
	void MyOnDestroy(){
		gameObject.SetActive(false);
		// TODO add different effects for fade away and hit(maybe even for different targets)
		// sound

		// particle
		Destroy(gameObject, destroyDelay);
	}

}
