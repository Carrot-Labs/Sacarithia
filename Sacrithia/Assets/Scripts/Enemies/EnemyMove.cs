using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	public bool standStill;
	public float speed;
	public bool isAttacking = false;
	public bool isDead = false;

	private Animator anim = null; 
	private Rigidbody2D rigidbody1;
	private bool facingBackwards = false;

	//at start of game
	void Start () {
		rigidbody1 = GetComponentInParent<Rigidbody2D> ();

		if(anim == null)
			anim = GetComponent<Animator> ();
	}

	//when player is detected in movement arrgo
	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			//figure out where player is
			Vector2 movement = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (other.transform.position.x, other.transform.position.y), speed * Time.deltaTime);

			//Turn to face the player
			if (movement.y > rigidbody1.position.y) {
				facingBackwards = true;
			} else if (movement.y <= rigidbody1.position.y) {
				facingBackwards = false;
			}

			//if dead, attacking or otherwise not allowed to move then pass your own transform else give the movement transform
			if (standStill || isDead || isAttacking) {
				rigidbody1.MovePosition (rigidbody1.position);
				Animate (rigidbody1.position);
			} else {
				rigidbody1.MovePosition (movement);
				Animate (movement);
			}
		}
	}

	//when player leaves the movement argo stop moving
	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			rigidbody1.MovePosition(rigidbody1.position);
			Animate(rigidbody1.position);
		}
	}

	//set properties to the animator
	void Animate(Vector2 position){
		anim.SetBool ("moving", (position.x != rigidbody1.position.x && position.y != rigidbody1.position.y));
		anim.SetBool("facingBackwards", facingBackwards);
		anim.SetBool ("dead", isDead);
		anim.SetBool ("attacking", isAttacking);
	}	

	//get the animator on the gameobject
	public Animator getAnimator(){
		if (anim == null)
			anim = GetComponent<Animator> ();
		return anim;
	}
}