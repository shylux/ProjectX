using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public bool respawnOnPickup = true;
	public float indicatorTriggerTime = 20f;

	private float _indicatorTimer = 0;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Score> ().scoreChange (1);

			if (respawnOnPickup) {
				GetComponent<PlaceRandomly> ().respawn ();
				//transform.Find ("Indicator").gameObject.SetActive (false);
				_indicatorTimer = 0;
			} else {
				Destroy (gameObject);
			}
		}
	}

	void Update() {
		if (indicatorTriggerTime == -1)
			// -1: disabled
			return;

		_indicatorTimer += Time.deltaTime;

		if (_indicatorTimer >= indicatorTriggerTime) {
			//transform.Find ("Indicator").gameObject.SetActive (true);
		}
	}
}
