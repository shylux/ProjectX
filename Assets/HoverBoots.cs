using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoverBoots : MonoBehaviour {

	private Rigidbody _rigid;

	void Start () {
		_rigid = GetComponent (typeof(Rigidbody)) as Rigidbody;
	}

	void Update () {
		
		if (!Physics.Raycast(transform.position, Vector3.down)) {
			Debug.Log ("Stop!");
			_rigid.useGravity = false;
			_rigid.velocity = Vector3.zero;
		} else {
			_rigid.useGravity = true;
		}
	}
}
