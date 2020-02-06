using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertRange : MonoBehaviour {

	public float value = 50;
	float oldMax = 200;
	float oldMin = 0;

	float newMax = 1;
	float newMin = 0;

	float newValue = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			newValue = Convert(newMin, newMax, oldMin, oldMax, value);
			Debug.Log("newValue: " + newValue);
		}
	}

	private float Convert(float newMin, float newMax, float oldMin, float oldMax, float value){
    // get Ranges
    float newRange = newMax - newMin;
    float oldRange = oldMax - oldMin;

	return newRange / oldRange * (value - oldMax) + 1;


	// if you want to clamp to new range
    // return Mathf.Clamp( newRange / oldRange * (value - oldMax) + 1, newMin, newMax) ;
	}
}
