using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageVectorTest : MonoBehaviour {

	public Transform position1;
	public Transform position2;

	[Range(0.0f, 1.0f)]
	public float weight = 0.5f;

	public float distance;
	public float distance1;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		Distance();
	}

	void ResetPosition(){
		transform.position = startPosition;
	}
	
	// Update is called once per frame
	void Update () {
		// Reset when R pressed
		if(Input.GetKey(KeyCode.R)){
			ResetPosition();
			Distance();
		}

		SetPosition();
	}

	// set new position by combining position1 and position2 with a weight towards position1
	// example weight 100 would be completly position1
	void SetPosition(){
		// Vector3 newPosition = Vector3.Lerp(startPosition, position1.position + position2.position, weight);
		Vector3 newPosition = Vector3.Lerp(position1.position, position2.position, weight);

		transform.position = newPosition;
		
	}

	void Distance(){
		distance = Vector3.Distance(position1.position, position2.position);
		distance1 = Vector3.Magnitude(position1.position - position2.position);
	}

}
