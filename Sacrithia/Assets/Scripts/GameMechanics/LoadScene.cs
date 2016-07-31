using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public string sceneToLoad;

	public float x = 0;
	public float y = 0;
	public float z = 0;

	void OnTriggerEnter2D(Collider2D other){
	
		if (other.CompareTag("Player")) {
			other.transform.position = new Vector3(x,y,z);
			Application.LoadLevel (sceneToLoad);
		}
	}
}
