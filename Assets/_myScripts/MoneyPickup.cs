using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour {

	public int amount = 10;
	public float destroyDelay = 0.5f;


	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")){
			// add to player inventory
			PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
			playerInventory.PickedUpMoney(amount);

			MyOnDestroy();
			
		}
	}

	void MyOnDestroy(){
		gameObject.SetActive(false);
		// TODO add effects
		Destroy(gameObject, destroyDelay);
	}
}