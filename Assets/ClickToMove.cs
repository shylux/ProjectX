using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.EventSystems;
using UnityEngine.VR.WSA.Input;

public class ClickToMove : MonoBehaviour {

	SpatialMappingManager spatialMappingManager;
	GestureRecognizer gestureRecognizer;
	Vector3 target = Vector3.zero;

	// Use this for initialization
	void Start () {
		spatialMappingManager = SpatialMappingManager.Instance;
		if (spatialMappingManager == null)
		{
			Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");
		}

		gestureRecognizer = new GestureRecognizer();
		gestureRecognizer.TappedEvent += OnTappedEvent;
		gestureRecognizer.StartCapturingGestures();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 headPosition = Camera.main.transform.position;
		Vector3 gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;
		if (Physics.Raycast (headPosition, gazeDirection, out hitInfo, 30.0f, spatialMappingManager.LayerMask)) {
			target = hitInfo.point;
		} else {
			target = Vector3.zero;
		}
	}

	public virtual void OnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay) {
		Debug.Log ("Tapped!");
		GetComponent<Arrive> ().setTarget (target);
	}
}
