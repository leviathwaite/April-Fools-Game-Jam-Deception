using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCard : MonoBehaviour {
	// number used to match with lock
	public int keyNumber = 1;

// TODO make light pulse between a range
// TODO collision with player for pickup

	Light light;
	GameObject player;

	// Use this for initialization
	void Start () {
		light = GetComponentInChildren<Light>();
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
        // Destroy(other.gameObject);
		// TODO should I disable instead of destroy?
		if (other.CompareTag("Player"))
  		{
			Invoke("DestroySelf", 5f);
			gameObject.SetActive(false);
			Inventory inventoryScript = (Inventory) other.GetComponent(typeof(Inventory));
			inventoryScript.PickupKey(keyNumber);
		}
    }

	void DestroySelf(){
		Destroy(gameObject);
	}
}
