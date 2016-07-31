using UnityEngine;
using System.Collections;

public class PlayerAttackZone : MonoBehaviour {

	public float delayTillMove;

	private PlayerMove movementScript;
	private PlayerStats statsScript;
	private EnemyHealth enemyHealth;
	private AudioSource audio;
	private float nextFire = 0.0F;

	void Start () {
		movementScript = GetComponentInParent<PlayerMove> ();
		statsScript = GetComponentInParent<PlayerStats> ();
		audio = GetComponent<AudioSource> ();
	}

	void Update(){
		transform.rotation = movementScript.currentHeading;

		bool currentAttack = Input.GetKeyDown (KeyCode.Space);

		if (currentAttack) {
			nextFire = Time.time + delayTillMove;
			attackEnemy();
			audio.Play();
		}

		if (Time.time < nextFire) {
			movementScript.isAttacking = true;
		} else {
			movementScript.isAttacking = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Enemy")) {
			enemyHealth = other.GetComponent<EnemyHealth>();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Enemy")){
			enemyHealth = null;
		}
	}

	void attackEnemy(){
		if (enemyHealth != null) {
			enemyHealth.Damage (statsScript.attackPower);
		}
	}
}