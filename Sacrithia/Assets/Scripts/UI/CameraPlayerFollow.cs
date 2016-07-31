using UnityEngine;
using System.Collections;

public class CameraPlayerFollow : MonoBehaviour {

	public float minx, maxx;
	public float miny, maxy;
	public float zOffset;

	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		float xPos = Mathf.Clamp (player.transform.position.x, minx, maxx);
		float yPos = Mathf.Clamp (player.transform.position.y, miny, maxy);

		transform.position = new Vector3 (xPos, yPos, zOffset);
	}
}
