﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraPan : MonoBehaviour {

	public float speed;
	public float range;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.Euler(0, Mathf.Sin(Time.realtimeSinceStartup * speed) * range, 0); 
    
	}

	void flipSpeed(){
		speed = -speed;
	}
}
