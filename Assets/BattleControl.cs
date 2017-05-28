﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.EventSystems;
using UnityEngine.VR.WSA.Input;

public class BattleControl : MonoBehaviour {

	public GameObject knight;
	public GameObject targetMarker;

	Camera cam;

	SpatialMappingManager spatialMappingManager;
	GestureRecognizer gestureRecognizer;

	// Use this for initialization
	void Start () {
		cam = Camera.main;

		spatialMappingManager = SpatialMappingManager.Instance;
		if (spatialMappingManager == null)
		{
			Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");
		}

		gestureRecognizer = new GestureRecognizer();
		gestureRecognizer.TappedEvent += OnTappedEvent;
		gestureRecognizer.StartCapturingGestures();
	}

	void Update () {

		// Target Marker
		Vector3 target = getTarget ();
		if (target != Vector3.zero && targetMarker != null) {
			targetMarker.transform.position = target;
		}

		if (Input.GetMouseButtonDown (0))
			OnTap();
	}

	Vector3 getTarget() {
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit)) {
			return hit.point;
		} else {
			return Vector3.zero;
		}
	}

	public virtual void OnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay) {
		OnTap ();
	}
	public void OnTap() {
		Debug.Log ("Tapped!");
		knight.GetComponent<Arrive> ().setTarget (getTarget ());
	}
}
