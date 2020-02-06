using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to track anything the player can pickup
public class PlayerInventory : MonoBehaviour {

	/* DEBUG */
	// starting values
	public int startingMoney = 10;
	public int startingAmmo = 100;

	public int ammo;
	public int money;
	// TODO does health go here? probably should only be health kits in inventory...


	// Use this for initialization
	void Start () {
		// TODO transfer inventory from storage for gameplay
		SetStartingQuantity();
	}

	void SetStartingQuantity(){
		ammo = startingAmmo;
		money = startingMoney;
	}
	

	/* AMMO */

	// do we have ammo
	public bool HaveAmmo(){
		return ammo > 0;
	}

	// use ammo
	public void UseAmmo(int amount){
		ammo -= amount;
	}

	// picked up ammo
	public void PickedUpAmmo(int amount){
		ammo += amount;
	}

	/* MONEY */

	// do we have money, NOt sure why we would need this
	public bool HaveMoney(){
		return money > 0;
	}

	// use money, TODO maybe weapon that fires money, stolen by enemy or can throw???
	public void UseMoney(int amount){
		money -= amount;
	}

	// picked up money
	public void PickedUpMoney(int amount){
		money += amount;
	}
}
