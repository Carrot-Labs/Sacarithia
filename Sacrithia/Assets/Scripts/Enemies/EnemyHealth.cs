using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int MAX_HEALTH;
	public int health;
	public int defense;

	public void ModifyingHealth(int value){
		health = healthCheck (health += value);
	}

	public void Damage(int value){
		health = healthCheck (health - (value - defense));
	}

	private int healthCheck(int h){

		if (h > MAX_HEALTH) {
			h =  MAX_HEALTH;
		} else if(h <= 0) {
			h = 0;
		}

		return h;
	}
}
