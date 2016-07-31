using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public bool playerDead = false;

	public int experiencePoints;
	public int MAX_EXPERIENCE;
	public int level;
	public int attackPower;
	public int defensePoints;
	public int MAX_SPECIAL;
	public int specialPoints;
	public int MAX_HEALTH;
	public int healthPoints;

	public Text healthText;
	public Text levelText;
	public Text specialText;

	void Start(){
		healthText.text = "HP: " + healthPoints + "/" + MAX_HEALTH;
		levelText.text = "Lv: " + level;
		specialText.text = "SP: " + specialPoints + "/" + MAX_SPECIAL;
	}

	public bool ModifyHealth(int value){
		healthPoints += value;

		if (healthPoints > MAX_HEALTH) {
			healthPoints = MAX_HEALTH;
		} else if (healthPoints <= 0) {
			playerDead = true;
			healthPoints = 0;
		}

		healthText.text = "HP: " + healthPoints + "/" + MAX_HEALTH;

		return playerDead;
	}

	public bool BeingAttacked(int value){
		if (value - defensePoints >= 0) {
			healthPoints -= (value - defensePoints);
		} else {
			healthPoints -= 0;
		}
		
		if (healthPoints > MAX_HEALTH) {
			healthPoints = MAX_HEALTH;
		} else if (healthPoints <= 0) {
			playerDead = true;
			healthPoints = 0;
		}
		
		healthText.text = "HP: " + healthPoints + "/" + MAX_HEALTH;
		
		return playerDead;
	}

	public void ModifyExpereince(int value){
		experiencePoints += value;
		if (experiencePoints > MAX_EXPERIENCE) {
			experiencePoints = experiencePoints - MAX_EXPERIENCE;
			LevelUp();
		}
	}

	public void ModifySpecial(int value){
		specialText.text = "SP: " + specialPoints + "/" + MAX_SPECIAL;
	}

	void LevelUp(){

		level++;

		MAX_HEALTH += 5;
		healthPoints = MAX_HEALTH;

		levelText.text = "Lv: " + level;
	}
}
