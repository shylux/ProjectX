using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {

	public GameObject knight;
	public GameObject targetMarker;

	Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;

	}

	void Update () {
		// Target Marker
		Vector3 target = getTarget ();
		if (target != Vector3.zero) {
			targetMarker.transform.position = target;
		}

		if (Input.GetMouseButtonDown(0))
			knight.GetComponent<Arrive> ().setTarget (getTarget ());
	}

	Vector3 getTarget() {
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit)) {
			return hit.point;
		} else {
			return Vector3.zero;
		}
	}
}
