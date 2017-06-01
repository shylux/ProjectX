using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarker : MonoBehaviour {

	void Update () {
		// Target Marker
		Vector3 target = getTarget ();
		if (target == Vector3.zero) {
			GetComponent<MeshRenderer> ().enabled = false;
		} else {
			GetComponent<MeshRenderer> ().enabled = true;
			transform.position = target;
		}

	}

	Vector3 getTarget() {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit)) {
			return hit.point;
		} else {
			return Vector3.zero;
		}
	}
}
