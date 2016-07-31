using UnityEngine;
using System.Collections;

public class EnemyDelayStart : MonoBehaviour {

	public float timeBeforeEnemyActive;

	private EnemyMove moveScript;
	private EnemyCommunication comScript;
	private EnemyAttack attackScript;
	private Collider2D entityCollider;
	private Animator anim;
	private float moveSpeed;
	private float initTime;
	private bool firstTimeinUpdate = true;
	private EnemyDelayArgo delayArgoScript;


	void Start () {
		moveScript = GetComponentInChildren<EnemyMove> ();
		comScript = GetComponent<EnemyCommunication> ();
		attackScript = GetComponentInChildren<EnemyAttack> ();
		delayArgoScript = GetComponentInChildren<EnemyDelayArgo> ();
		entityCollider = GetComponent<Collider2D> ();

		//set inital states
		transform.position = new Vector3(transform.position.x,transform.position.y,0.1f);
		attackScript.enabled = false;
		comScript.enabled = false;
		entityCollider.enabled = false;
		anim = moveScript.getAnimator ();
		moveSpeed = moveScript.speed;
		moveScript.speed = 0;
		moveScript.isDead = true;

		initTime = Time.time;
	}

	void Update () {
		//switch to regular enemey attack patterns
		if (initTime + timeBeforeEnemyActive < Time.time) {
			transform.position = new Vector3(transform.position.x,transform.position.y,0f);
			attackScript.enabled = true;
			entityCollider.enabled = true;
			moveScript.isDead = false;
			comScript.enabled = true;
			moveScript.speed = moveSpeed;
			delayArgoScript.enabled = false;
			this.enabled = false;
		}
		//currently we are delaying the moving and attacking for some reason
		else { 
			if(delayArgoScript.playerEnteredZone){
				if(firstTimeinUpdate){
					anim.SetBool("awake",true);
					firstTimeinUpdate = false;
				}
			} else{
				initTime = Time.time;
			}
		}
	}
}
