using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyGoap : Enemy {

	public override void passiveRegen(){

	}

	public override HashSet<KeyValuePair<string, object>> createGoalState(){
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>> ();

		// add goals here
		/* 
		goal.Add (new KeyValuePair<string, object> ("damagePlayer", true));
		goal.Add (new KeyValuePair<string, object> ("stayAlive", true));
		*/
		

		return goal;
	}
}
