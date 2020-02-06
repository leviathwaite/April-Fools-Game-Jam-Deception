using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to tell player he has been seen
public class PlayerStatus : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// called when player is spotted
	public void Spotted(){
		Debug.Log("Player Spotted");
	}
}
