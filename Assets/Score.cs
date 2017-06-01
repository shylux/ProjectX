using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	private int _score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void scoreChange(int change) {
		_score += change;
		Debug.Log ("New Score: " + _score);
	}
}
