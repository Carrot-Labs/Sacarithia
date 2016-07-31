using UnityEngine;
using System.Collections;

public class EnemyArrowFire : MonoBehaviour {

	public float speed;
	public int attackPower;
	public GameObject shooter;

	private Vector2 target;
	private Rigidbody2D rb;
	private EnemyHealth health;
	private Sprite sprite;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		health = GetComponent<EnemyHealth> ();

		GameObject player = GameObject.FindGameObjectWithTag ("Player");;
		target = new Vector2(player.transform.position.x, player.transform.position.y);

		transform.rotation = CalculateCurrentHeading (target);
	}
	
	// Update is called once per frame
	void Update () {
		if (health.health > 0) {
			rb.MovePosition (Vector2.MoveTowards (rb.position, target, Time.deltaTime * speed));

			if (rb.position.x == target.x && rb.position.y == target.y) {
				GetComponent<Collider2D> ().isTrigger = true;
			}
		} else {
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			//attack then die
			other.gameObject.GetComponent<PlayerStats> ().BeingAttacked (attackPower);
			Destroy (this.gameObject);
		} else if (!other.gameObject.CompareTag("Enviroment")) {
			//we collided with some object in enviroment
			Destroy (this.gameObject);
		} else if (other.gameObject.CompareTag("Enemy") && !other.gameObject.Equals(shooter)){
			//we damage the other enemy if it is not the shooter
			other.gameObject.GetComponent<EnemyHealth>().Damage(attackPower);
			Destroy (this.gameObject);
		}
	}

	Quaternion CalculateCurrentHeading(Vector2 target){
	
		Vector2 currentPosition = new Vector2 (transform.position.x, transform.position.y);
		float zet = 0;

		if (target.y > currentPosition.y) {
			if(target.x > currentPosition.x){
				if(Mathf.Abs(target.x - currentPosition.x) > Mathf.Abs(target.y - currentPosition.y)){
					zet = 0;
				} else {
					zet = 90;
				}
			} else {
				if(Mathf.Abs(target.x - currentPosition.x) > Mathf.Abs(target.y - currentPosition.y)){
					zet = 180;
				} else {
					zet = 90;
				}
			}
		} else {
			if(target.x > currentPosition.x){
				if(Mathf.Abs(target.x - currentPosition.x) > Mathf.Abs(target.y - currentPosition.y)){
					zet = 0;
				} else {
					zet = 270;
				}
			} else {
				if(Mathf.Abs(target.x - currentPosition.x) > Mathf.Abs(target.y - currentPosition.y)){
					zet = 180;
				} else {
					zet = 270;
				}
			}
		}

		Quaternion calcHeading = Quaternion.Euler(0f,0f,zet);
		return calcHeading;
	}
}



