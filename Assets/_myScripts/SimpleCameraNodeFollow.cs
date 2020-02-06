using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraNodeFollow : MonoBehaviour {

	public Vector3 positionOffset;
	public Vector3 rotationOffset;


	Transform target;
	Transform player;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.position + positionOffset;

		transform.rotation = Quaternion.Euler(
			rotationOffset.x,
			rotationOffset.y,
			rotationOffset.z);
		}
}
