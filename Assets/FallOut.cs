using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOut : MonoBehaviour {

	void Update () {
		if (!Physics.Raycast (transform.position, Vector3.down)) {
			// Whoa there is no ground below us!
			Debug.Log("Fell out. Respawn!");
			GetComponent<PlaceRandomly>().respawn();
		}
	}
}
