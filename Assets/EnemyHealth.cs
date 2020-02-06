using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class EnemyHealth : MonoBehaviour {

	// foreground rect transform
	public RectTransform healthBar;

	[SerializeField]
	private int startingHealth = 100;

	[SerializeField]
	private int currentHealth;

	private Rigidbody rb;
	private NavMeshAgent agent;

	// Time after being hurt til can be hurt again TODO does enemy get this???
	private bool hurt;
	private float hurtTime = 60;
	private float hurtTimer;

	private bool alive;

	private Animator anim;



	// Use this for initialization
	void Start () {
		alive = true;
		currentHealth = startingHealth;
		anim = GetComponentInParent<Animator>();
		if(anim == null){
			Debug.LogError("EnemyHealth Animator, NULL");
		}else{
			// check for Dead state
			int deadHash = Animator.StringToHash("Dead");
			if(!anim.HasState(0, deadHash)){
				Debug.LogError("EnemyHealth, Dead State does not exist on Animator base layer");
			}
		}

		rb = GetComponent<Rigidbody>();

		agent = GetComponent<NavMeshAgent>();

		// setup healthbar
		// healthBar = GetComponentInChildren<RectTransform>();
	}

	void resetHurtTimer(){
		// hurtTimer equals current time plus hurtTime amount
		hurtTimer = hurtTime + Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (alive) {
			// hurt timer
			if(hurt){
				tickHurtTimer();
			}
			if(currentHealth <= 0){
				currentHealth = 0;
				alive = false;
				anim.SetBool("dead", true);
			}
		}

		UpdateAnimator();
	}

	void UpdateAnimator(){
		anim.SetInteger("health", currentHealth);
	}

	// used to count down hurt timer until can be hurt again
	void tickHurtTimer(){
		if(hurtTimer < Time.time){
			hurt = false;
		}
	}

	// used to add health, example is from health pickups
	public void AddHealth(int amount){
		if (alive) {
			currentHealth += amount;
			healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
		}
	}

	// used to decrement health, amount meant to be positive
	public void TakeHealth(int amount){
		// if we are alive 
		// and not recently hurt
		// if (alive && !hurt) {
		if(alive){
			hurt = true;
			currentHealth -= amount;

			// disable ridgid body rotation locks on killing hit
			if(currentHealth <= 0){
				// rb.constraints = RigidbodyConstraints.FreezeAll;
				rb.constraints = RigidbodyConstraints.None;

				// remove health bar
				GameObject healthBarParent = healthBar.parent.gameObject;
				healthBarParent.SetActive(false);
			}

		}
		Debug.Log ("Update health bar");
		healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
	}


	// use for character controller and/or animator
	public bool IsAlive(){
		return alive;
	}
}
