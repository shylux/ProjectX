using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.EventSystems;
using UnityEngine.VR.WSA.Input;

public class BattleControl : MonoBehaviour {

	public GameObject knight;
	public GameObject slime;
	public GameObject coinPrefab;

	public float coinSpawnRadius = 2;

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
		if (Input.GetMouseButtonDown (0))
			OnTap();
	}
	public virtual void OnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay) {
		OnTap ();
	}

	Vector3 getTarget() {
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit)) {
			return hit.point;
		} else {
			return Vector3.zero;
		}
	}

	public void OnTap() {
		Debug.Log ("Tapped!");
		if (getTarget () != Vector3.zero) {
			if (GazeManager.Instance.HitObject.tag == "Player") {
				foreach (GameObject coin in GameObject.FindGameObjectsWithTag ("Coin")) {
					coin.GetComponent<PlaceRandomly> ().respawn ();
					slime.transform.rotation *= Quaternion.AngleAxis (180, Vector3.right);
				}
			} else {
				slime.GetComponent<Arrive> ().setTarget (getTarget ());
			}
		}
	}
}
