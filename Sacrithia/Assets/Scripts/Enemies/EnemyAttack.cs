using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	
	public float fireRate;
	public int attackDamage;
	public bool isRanged;
	public bool isDead = false;
	public bool isAttacking = false;
	public GameObject rangedItem;
	public GameObject frontLaunchPoint;
	public GameObject backLaunchPoint;

	private float nextFire = 0.0F;
	private GameObject player;

	void Update(){
		if (!isDead) {
			if (Time.time > nextFire && isAttacking) {
				if(isRanged){
					Vector3 playerPosition = new Vector3(player.transform.position.x,player.transform.position.y,0f);
					GameObject arrow = null;
					if(playerPosition.y > transform.position.y){
						arrow = (GameObject)Instantiate(rangedItem,backLaunchPoint.transform.position, backLaunchPoint.transform.rotation);
					}else{
						arrow = (GameObject)Instantiate(rangedItem,frontLaunchPoint.transform.position, frontLaunchPoint.transform.rotation);
					}

					EnemyArrowFire arrowScript = arrow.GetComponent<EnemyArrowFire>();
					arrowScript.shooter = this.gameObject;
					nextFire = Time.time + fireRate;
				} else {
					PlayerStats playerStats = player.GetComponent<PlayerStats> ();
					playerStats.BeingAttacked (attackDamage);
					nextFire = Time.time + fireRate;
				}
			}
		}
	}

	//player has entered the attack range now wanting to attack
	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			player = other.gameObject;
			isAttacking = true;
		}
	}

	//Player has left the attack range
	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			isAttacking = false;
		}
	}
}
