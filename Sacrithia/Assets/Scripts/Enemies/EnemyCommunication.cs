using UnityEngine;
using System.Collections;

public class EnemyCommunication : MonoBehaviour {

	public float timeTillDeath;
	public float deathAnimationTime;
	public GameObject deathAnimationParticles;

	private bool isAttacking = false;
	private bool isDead = false;
	private float currentTime;
	private EnemyMove movementScript;
	private EnemyAttack attackScript;
	private EnemyHealth healthScript;
	
	void Start () {
		movementScript = GetComponentInChildren<EnemyMove> ();
		attackScript = GetComponentInChildren<EnemyAttack> ();
		healthScript = GetComponent<EnemyHealth> ();
	}

	void Update () {
		isDead = healthScript.health <= 0;
		movementScript.isDead = isDead;
		attackScript.isDead = isDead;

		isAttacking = attackScript.isAttacking;
		movementScript.isAttacking = isAttacking;

		if (isDead) {
			if(currentTime + timeTillDeath < Time.time){
				Destroy (Instantiate (deathAnimationParticles, transform.position, transform.rotation), deathAnimationTime);
				Destroy (this.gameObject);
			}
		} else {
			currentTime = Time.time;
		}
	}
}
