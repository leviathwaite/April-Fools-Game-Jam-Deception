using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPistol : MonoBehaviour {
	Rigidbody playerRB;
	PlayerInventory playerInventory;
	// used to adjust where bullet is created in relation to gun
	// bullet to be created
	Transform barrel;
	public GameObject bullet;

	// kick back
	public float maxKickBack = 1;

	// TODO setup recoil

	// how many bullets are used each time gun is fired
	public int fireRate = 1;

	// Recoil Rotation
	public float sweepRange = 10;
	public float sweepSpeed = 10;


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
			// create bullet at guns position plus offset, with guns rotation
			Instantiate(bullet, barrel.position, transform.rotation); // RecoilRotation());
		}
	}

	// needs fixed, rotates to wrong angles
	Quaternion RecoilRotation(){

		float sweep;
		sweep = (-sweepRange) / 2 + Mathf.PingPong(Time.time * sweepSpeed, (sweepRange));
	
		
		// uses sweep range as full range(negative and postive sumed)
	
		// uses sweep range as an extent from zero(ex sweepRange = 45 actual range = 90, from -45 to 45)
		// sweep = (-sweepRange) + Mathf.Repeat(Time.time * sweepSpeed, (sweepRange * 2));
		Vector3 currentRotation = transform.localEulerAngles;
		Quaternion	targetRotation = Quaternion.AngleAxis(sweep + currentRotation.y, Vector3.up);
		return targetRotation;
		
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
