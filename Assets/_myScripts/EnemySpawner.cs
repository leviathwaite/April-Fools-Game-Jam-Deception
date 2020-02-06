using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] spawnPoints;
	public int numberOfSpawnPoints;
	public GameObject[] enemies;
	public int typesOfEnemies;
	
	public int numberOfEnemies = 100; // TODO never ending

	public float timeBetweenSpawns = 5;
	public float spawnTimer;


	// Use this for initialization
	void Start () {
		// TODO rename enemy spawn points
		// find spawnpoints
		spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		numberOfSpawnPoints = spawnPoints.Length;
		typesOfEnemies = enemies.Length;

		ResetSpawnTimer();
	}

	void ResetSpawnTimer(){
		// set spawnTimer to a future time
		spawnTimer = timeBetweenSpawns + Time.time;
	}
	
	void Update(){
		TickSpawnTimer();
	}

	void TickSpawnTimer(){
		// Times up
		if(spawnTimer < Time.time){
			SpawnWave();
		}
	}

	void SpawnWave(){
		Debug.Log("Spawning Wave");
		// TODO direction to player

		// Reset SpawnTimer
		ResetSpawnTimer();

		for(int i = 0; i < numberOfSpawnPoints; i++){
			// TODO change to more strutured random, with direction toward player

			// Random.Range(0, typesOfEnemies)
			Instantiate(
				enemies[0], 
				spawnPoints[i].transform.position,
				Quaternion.identity);
		}
	}
	
}
