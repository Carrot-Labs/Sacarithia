using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float playerSpeed = 3;
	public bool isAttacking = false;
	public Quaternion currentHeading;

	Vector2 playerMovement;
	Rigidbody2D playerRigidbody;
	Animator anim;    
	bool facingBackwards = false;
	PlayerStats stats;
	
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		currentHeading = Quaternion.Euler (0f, 0f, 0f);
		stats = GetComponent<PlayerStats> ();
	}

	void Update () {
		if (stats.playerDead) {
			Animating (0, 0, true);
		} else {
			float horizontal = Input.GetAxisRaw ("Horizontal");
			float vertical = Input.GetAxisRaw ("Vertical");

			CalculateCurrentHeading (new Vector2 (horizontal, vertical));

			if (isAttacking) {
				Move (0f, 0f);
			} else {
				Move (horizontal, vertical);
			}

			Animating (horizontal, vertical, false);
		}
	}

	void Move (float h, float v)
	{
		playerMovement.Set (h, v);

		playerMovement = playerMovement.normalized * playerSpeed * Time.deltaTime;

		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		playerRigidbody.MovePosition (pos + playerMovement);
	}

	void Animating (float h, float v,bool dead)
	{
		if (!dead) {
			bool walking = h != 0f || v != 0f;

			if (v > 0f) {
				facingBackwards = true;
			} else if (v < 0f) {
				facingBackwards = false;
			}

			anim.SetBool ("moving", walking);
			anim.SetBool ("facingBackwards", facingBackwards);
			anim.SetBool ("attacking", isAttacking);
			anim.SetBool("dead",false);
		} else {
			anim.SetBool ("moving", false);
			anim.SetBool ("facingBackwards", facingBackwards);
			anim.SetBool ("attacking", false);
			anim.SetBool("dead",true);
		}
	}

	void CalculateCurrentHeading(Vector2 move){

		//going right
		if (move.x == 1f && move.y == 0f) {
			currentHeading = Quaternion.Euler(0f,0f,90f);
		}
		//going diagonal up right
		else if (move.x == 1f && move.y == 1f) {
			currentHeading = Quaternion.Euler(0f,0f,135f);
		}
		//going up
		else if (move.x == 0f && move.y == 1f) {
			currentHeading = Quaternion.Euler(0f,0f,180f);
		}
		//going diagonal up left
		else if (move.x == -1f && move.y == 1f) {
			currentHeading = Quaternion.Euler(0f,0f,225f);
		}
		//going left
		else if (move.x == -1f && move.y == 0f) {
			currentHeading = Quaternion.Euler(0f,0f,270f);
		}
		//going diagonal down left
		else if (move.x == -1f && move.y == -1f) {
			currentHeading = Quaternion.Euler(0f,0f,315f);
		}
		//going down
		else if (move.x == 0f && move.y == -1f) {
			currentHeading = Quaternion.Euler(0f,0f,0f);
		}
		//going diagonal down right
		else if (move.x == 1f && move.y == -1f){
			currentHeading = Quaternion.Euler(0f,0f,45f);
		}
		//else it will stay previous if there is an error or it is 0f, 0f
	}
}
