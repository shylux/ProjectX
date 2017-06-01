using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlaceRandomly : MonoBehaviour {

	public bool toBePlaced = true;
	public float radiusRange = 1;
	public GameObject relativeTo;

	public void respawn() {
		// make sure the coin cant be collected while we try to place it
		transform.position += Vector3.up * 100000;
		toBePlaced = true;
	}

	void Update () {
		// try to place once per frame
		if (toBePlaced) {
			Vector3 randLocation = Random.insideUnitSphere * radiusRange;
			randLocation.y = 0;

			if (relativeTo)
				randLocation = randLocation + relativeTo.transform.position;

			RaycastHit hit;
			if (Physics.Raycast (randLocation, Vector3.down, out hit)) {
				
				if (Vector3.Angle(Vector3.up, hit.normal) > 45)
					// only place on horizontal surfaces
					return;
				
				transform.position = hit.point + Vector3.up;
				toBePlaced = false;
			}
		}
	}
}
