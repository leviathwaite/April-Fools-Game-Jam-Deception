using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public int[] keys;
	private int numberOfKeys;
	private int keyCounter;

	void Start(){
		numberOfKeys = 10; // random starting value
		keys = new int[numberOfKeys];
		// init keys
		for(int i = 0; i < numberOfKeys; i ++){
			keys[i] = -1;
		}
		// init keyCounter to track next avail spot
		keyCounter = 0;
	}

	// TODO create pickup method for security card and a place to store code for matching with lock
	public void PickupKey(int keyNumber){
		keys[keyCounter] = keyNumber;
		keyCounter++;
		if(keyCounter > numberOfKeys){
			Debug.Log("Inventory for keys needs to be resized");
		}
	}

	// TODO create method to call from lock collision to check for key

}
