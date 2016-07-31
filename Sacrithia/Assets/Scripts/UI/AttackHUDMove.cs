using UnityEngine;
using System.Collections;

public class AttackHUDMove : MonoBehaviour {

	PlayerMove movementScript;
	
	void Start () {
		movementScript = GetComponentInParent<PlayerMove> ();
	}
	
	void Update(){
		transform.rotation = movementScript.currentHeading;
	}
}
