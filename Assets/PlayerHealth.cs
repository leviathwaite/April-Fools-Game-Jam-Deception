using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO require Animator
// also requires a Dead bool inside animator
public class PlayerHealth : MonoBehaviour {

	public RectTransform healthBar;

	[SerializeField]
	private float startingHealth = 100f;

	[SerializeField]
	private float currentHealth;

	// Time after being hurt til can be hurt again
	private bool hurt;
	private float hurtTime = 60; // measured in frames TODO change to seconds
	private float hurtTimer;

	private bool alive;

	private Animator anim;



	// Use this for initialization
	void Start () {
		alive = true;
		currentHealth = startingHealth;
		anim = GetComponentInParent<Animator>();
		if(anim == null){
			Debug.LogError("PlayerHealth Animator, NULL");
		}else{
			// check for Dead state
			int deadHash = Animator.StringToHash("Dead");
			if(!anim.HasState(0, deadHash)){
				Debug.LogError("PlayerHealth, Dead State does not exist on Animator base layer");
			}
		}
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
				anim.SetBool("Dead", true);
			}
		}
	}

	// used to count down hurt timer until player can be hurt again
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
		// if we are alive and not recently hurt
		if (alive && !hurt) {
			hurt = true;
			currentHealth -= amount;
			Debug.Log ("Update health bar");
			healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
		}
	}

	// use for character controller and/or animator
	public bool IsAlive(){
		return alive;
	}
}
