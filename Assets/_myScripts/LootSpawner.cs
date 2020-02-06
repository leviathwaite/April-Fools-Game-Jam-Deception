using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour {

	public GameObject[] loot;
	public GameObject[] pickups;

	public void spawnLoot(Transform t){
		// TODO random or balanced random loot drop
		// TODO should random chance be handled here? probably
		Instantiate(loot[Random.Range(0, loot.Length)], t.position, Quaternion.identity);
	}


}
