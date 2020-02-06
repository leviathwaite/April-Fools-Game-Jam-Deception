using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SweepMovement : MonoBehaviour {


	 //Define Enum
     public enum SweepTypeEnum{PingPong, Repeat};
     
     //This is what you need to show in the inspector.
     public SweepTypeEnum sweepType;

	 // Only works with Repeat not Pingpong, DUH
	 [SerializeField]
	private bool sweepLeftToRight = true;
	

	[SerializeField]
	private float sweepRange = 45;

	[SerializeField]
	private float sweepSpeed;
	private Vector3 startingRotation;

	// Use this for initialization
	void Start () 
	{
		//relative to world, but in Quaternion
		// transform.rotation

		// World space
		// transform.eulerAngles = transform.rotation.eulerAngles

		// Local space, same as inspector
		// transform.localEulerAngles = transform.localRotation.eulerAngles;

		// use for smoothing
		// Mathf.SmoothDamp();

		startingRotation = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		Sweep();
	}

	void Sweep()
	{

		// use sweep range for + & - ranges, using Repeat or PingPong
		float sweep;
		if(sweepType == SweepTypeEnum.PingPong)
		{
			// works
			sweep = (-sweepRange) / 2 + Mathf.PingPong(Time.time * sweepSpeed, (sweepRange));
		}
		else
		{
			if(sweepLeftToRight){
				sweep = (-sweepRange) / 2 + Mathf.Repeat(Time.time * sweepSpeed, (sweepRange));
			}else{
				// right to left
				// sweep opposite direction
				sweep = (sweepRange) / 2 - Mathf.Repeat(Time.time * sweepSpeed, (sweepRange));
			}
			
			// uses sweep range as full range(negative and postive sumed)
		
			// uses sweep range as an extent from zero(ex sweepRange = 45 actual range = 90, from -45 to 45)
			// sweep = (-sweepRange) + Mathf.Repeat(Time.time * sweepSpeed, (sweepRange * 2));
		}
			Quaternion	targetRotation = Quaternion.AngleAxis(sweep + startingRotation.y, Vector3.up);
			transform.rotation = targetRotation;
			
			// Continually spins
			// transform.rotation *= targetRotation;
			// transform.Rotate(Vector3.up, sweep + startingRotation.y);
	}
}
