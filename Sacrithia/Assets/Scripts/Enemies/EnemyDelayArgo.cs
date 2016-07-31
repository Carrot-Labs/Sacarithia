using UnityEngine;
using System.Collections;

public class EnemyDelayArgo : MonoBehaviour {

	public bool playerEnteredZone = false;

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			playerEnteredZone = true;
		}
	}
}
