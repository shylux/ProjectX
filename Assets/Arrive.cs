using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrive : MonoBehaviour {

	private Vector3 _target = Vector3.zero;
	private Rigidbody rigid;

	public float rotationSpeed = 20f;
	public float movementSpeed = 0.00001f;

	// Use this for initialization
	void Start () {
		rigid = GetComponent (typeof(Rigidbody)) as Rigidbody;
	}

	public void setTarget(Vector3 target) {
		this._target = target;
	}

	void FixedUpdate () {
		if (_target == Vector3.zero)
			return;

		Debug.Log ((_target - transform.position).magnitude);
		if ((_target - transform.position).magnitude > 0.1)
			moveTowardsTarget ();
	}

	void moveTowardsTarget() {
		Vector3 direction = _target - transform.position;
		direction.y = 0;

		Vector3 newRotation = Vector3.RotateTowards (transform.forward, direction, rotationSpeed * Time.deltaTime, 0f);
		transform.rotation = Quaternion.LookRotation (newRotation);

		if (Vector3.Angle (transform.forward, direction) < 10) {
			// moving
			transform.position += direction.normalized * movementSpeed;
		}
	}
}
