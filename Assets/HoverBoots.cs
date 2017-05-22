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
		_rigid.useGravity = Physics.Raycast(transform.position + Vector3.up, Vector3.down);
	}
}
