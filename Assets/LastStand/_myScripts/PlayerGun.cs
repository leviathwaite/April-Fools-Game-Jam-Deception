using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Created to be the parent of player guns. TODO create scriptable object weapons
 */

public class PlayerGun : MonoBehaviour {

	// ref to player's rigidBody
	Rigidbody playerRB;

	// ref to player's inventory, where bullets are stored
	PlayerInventory playerInventory;

	// where the bullets will appear
	Transform barrel;
	// the bullet
	public GameObject bullet;

	//======================= Gun Stats ==========================//
	float maxKickBack;
	// how many bullets are used each time gun is fired
	public int fireRate = 1;

	// Recoil Rotation
	public float maxBulletStray;


	// amount of time between firing
	public float timeBetweenFiring = 1;
	// count down timer til can fire again
	public float fireTimer;

	// Use this for initialization
	void Start () {
		playerRB = GetComponentInParent<Rigidbody>();
		// TODO assign bullet here instead of drag and drop into inspector

		// get the player inventory for access to bullets
		playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

		// setup barrel
		barrel = transform.Find("Barrel").transform;
		// reset the timer at start
		ResetFiring();
	}

	// used to reset timer for next shot
	void ResetFiring(){
		fireTimer = timeBetweenFiring + Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		// if the fire button pressed and can shoot
		if(Input.GetButton("Fire1") && fireTimer < Time.time){
			if(playerInventory.HaveAmmo()){
				Shoot();
			}else{
				// TODO play out of ammo noise
			}
			
		}
	}

	// create bullet
	void Shoot(){
		// kick back
		KickBack();
		// tick ammo
		TickAmmo();
		// reset firing timer
		ResetFiring();
		// create number of bullets set in firerate
		// TODO recoil rotation offset for faster guns
		for(int i = 0; i < fireRate; i ++){
			// TODO add pooling...
			// create bullet at guns position plus offset, with guns rotation
			GameObject newBullet = Instantiate(bullet, barrel.position, transform.rotation); // RecoilRotation());
			// TODO use maxBulletStray to add angle to bullets
			float stray = Random.Range(-maxBulletStray, maxBulletStray);
			newBullet.transform.Rotate(0, stray, 0);
		}
	}


	void KickBack(){
		// force to kick back with
		float kickBack = Random.Range(maxKickBack / 10, maxKickBack);
		// whatever force is left over is put into hop force
		float hop = maxKickBack - kickBack;
		Vector3 kickDirection = (-transform.forward * kickBack) + (transform.up * hop);

		playerRB.AddForce(kickDirection, ForceMode.Impulse);
	}

	void TickAmmo(){
		// tell player inventory we used bullet/bullets
		playerInventory.UseAmmo(fireRate);
	}
}
