using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Camera control script.
 * Use WASD to move the camera.
 * Hold right mouse button and move mouse to change perspective.
 */
public class IntuitiveCamera : MonoBehaviour {

	float speed = 1.0f / 60;

	[Tooltip("Acceleration per second")]
	float acceleration = 2f;

	int framesOfMovement = 0;

	Vector3 rotation = Vector3.zero;
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = Input.GetAxis ("Mouse X");
		float y = Input.GetAxis ("Mouse Y");

		// ROTATION
		if (Input.GetMouseButton (1)) {
			UnityEngine.Cursor.lockState = CursorLockMode.Locked;

			rotation.x -= y;
			rotation.y += x;
			rotation.x = Mathf.Clamp (rotation.x, -89, 89);
			transform.rotation = Quaternion.Euler (rotation);
		} else {
			UnityEngine.Cursor.lockState = CursorLockMode.None;
		}

		// MOVEMENT
		Vector3 absoluteMovement = Vector3.zero;
		Vector3 relativeMovement = Vector3.zero;

		if (Input.GetKey (KeyCode.Space))
			absoluteMovement += Vector3.up;
		if (Input.GetKey (KeyCode.LeftShift))
			absoluteMovement += Vector3.down;

		if (Input.GetKey (KeyCode.W))
			relativeMovement += Vector3.forward;
		if (Input.GetKey (KeyCode.A))
			relativeMovement += Vector3.left;
		if (Input.GetKey (KeyCode.S))
			relativeMovement += Vector3.back;
		if (Input.GetKey (KeyCode.D))
			relativeMovement += Vector3.right;

		// acceleration
		if (absoluteMovement.magnitude + relativeMovement.magnitude != 0)
			framesOfMovement++;
		else
			framesOfMovement = 0;

		// relative movement is in look direction on the xz-plane
		relativeMovement = Vector3.ProjectOnPlane (transform.rotation * relativeMovement, Vector3.up);

		// set max movement
		Vector3 movement = absoluteMovement.normalized + relativeMovement.normalized;
		movement = movement.normalized * speed * getAcceleration();

		this.transform.position += movement;
	}

	float getAcceleration() {
		return 1 + acceleration * framesOfMovement / 60; //TODO use delta time
	}
}
