using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME require light

public class MoodLightController : MonoBehaviour {


	//Define Enum
     public enum MoodStatesEnum{NORMAL, ALERTED, SUSPICOUS};
     
     //TODO change access for moodState
     public MoodStatesEnum moodState;
	 private MoodStatesEnum prevMood;

	 private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		Reset();
	}

	void Reset(){
		setMood(MoodStatesEnum.NORMAL);
	}

	// Update is called once per frame
	void Update () {
		// TODO move from checking to called???
		// mood changed
		if(prevMood != moodState){
			// update prevMood
			prevMood = moodState;
			// update mood
			setMood(moodState);

		}
	}

	public void setMood(MoodStatesEnum newState){
			// set moodState
			moodState = newState;
			// call mood
			switch(newState){
				// In searching mode
				case MoodStatesEnum.SUSPICOUS:
					Suspicous();
					break;
				// on the trail or line of sight
				case MoodStatesEnum.ALERTED: 
					Alerted();
					break;

				// MoodStatesEnum.NORMAL:
				default: 
					Normal();
					break;
			
		}
	}

	private void Suspicous(){
		light.intensity = 5;
		light.color = Color.yellow;
	}

	private void Alerted(){
		light.intensity = 10;
		light.color = Color.red;
	}

	private void Normal(){
		light.intensity = 0;
		light.color = Color.white;
	}
}
