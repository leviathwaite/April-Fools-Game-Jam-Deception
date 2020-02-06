using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO pool you fools
public class PlayerBullet : MonoBehaviour {
	public float speed = 10f;
	// strength
	public int damage = 33;
	// amount of time
	public float lifeTime = 5;
	// timer
	public float lifeTimer;
	// delay for Destroy()
	public float destroyDelay = 0.5f;

	// Use this for initialization
	void Start () {
		lifeTimer = Time.time + lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		// Move bullet
		Move();
		// check lifeTimer
		TickTimer();
	}

	// move in forward direction
	void Move(){
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	void TickTimer(){
		if(lifeTimer < Time.time) {
			// destory
			MyOnDestroy();
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Player Bullet Trigger Entered, " + other.tag);
		if(other.CompareTag("Enemy")){
			if(other.GetComponent<EnemyHealth>()){
				// hurt enemy by bullet damage
				other.GetComponent<EnemyHealth>().TakeHealth(damage);
				// get collision point and add force to enemy rigidbody at that point
				other.attachedRigidbody.AddExplosionForce( 3, transform.position, 1, 2, ForceMode.Impulse);
			}
			MyOnDestroy();
		}
}

/* 
	void OnCollisionEnter(Collision collision) {
		Debug.Log("Player Bullet On Collision Enter");
		if(collision.collider.CompareTag("Enemy")){
			MyOnDestroy();
			
		}
	}
	
*/
	// used to play effects when gameobject is destroyed
	void MyOnDestroy(){
		gameObject.SetActive(false);
		// TODO add different effects for fade away and hit(maybe even for different targets)
		// sound

		// particle
		Destroy(gameObject, destroyDelay);
	}
}
