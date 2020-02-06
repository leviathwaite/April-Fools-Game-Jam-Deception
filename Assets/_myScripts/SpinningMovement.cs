using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningMovement : MonoBehaviour {
	public Vector3 startRotation;

	public Vector3 rotationSpeed;


	// Use this for initialization
	void Start () {
		transform.Rotate(startRotation);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotationSpeed);
	}
}
