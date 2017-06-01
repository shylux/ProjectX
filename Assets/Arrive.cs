using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrive : MonoBehaviour {

	public float rotationSpeed = 4f;
	public float movementSpeed = 2f;

	private Vector3 _target = Vector3.zero;
	private float _expectedTravelTime;
	private float _currentTravelTime;
	private Animation _anim;

	void Start() {
		_anim = GetComponent<Animation> () as Animation;
		_anim.wrapMode = WrapMode.Loop;
	}

	public void setTarget(Vector3 target) {
		Debug.Log ("Arrive: target set");
		_target = target;
		_expectedTravelTime = (_target - transform.position).magnitude / movementSpeed;
		_currentTravelTime = 0f;

		_anim.CrossFade ("Walk");
	}

	void FixedUpdate () {
		if (_target == Vector3.zero)
			return;

		if (_currentTravelTime > _expectedTravelTime * 1.2 || isUnderOverTarget()) {
			// We ran into an obstacle. Warp and finish
			transform.position = _target;
			arrivedAtTarget ();
			return;
		}

		if ((_target - transform.position).magnitude > 0.1)
			moveTowardsTarget ();
		else
			// finish
			arrivedAtTarget();
	}

	void arrivedAtTarget() {
		_target = Vector3.zero;
		_anim.CrossFade ("Wait");
	}

	void moveTowardsTarget() {
		Vector3 direction = _target - transform.position;
		direction.y = 0;

		Vector3 newRotation = Vector3.RotateTowards (transform.forward, direction, rotationSpeed * Time.deltaTime, 0f);
		transform.rotation = Quaternion.LookRotation (newRotation);

		if (Vector3.Angle (transform.forward, direction) < 10) {
			// moving
			transform.position += direction.normalized * movementSpeed * Time.deltaTime;
			// we only count moving time to travel time
			_currentTravelTime += Time.deltaTime;
		}
	}

	bool isUnderOverTarget() {
		return (Vector3.ProjectOnPlane(transform.position, Vector3.up) - Vector3.ProjectOnPlane(_target, Vector3.up)).magnitude < 0.01;
	}
}
