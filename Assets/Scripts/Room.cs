using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	public Connection[] connections;
	public List<Transform> availLeakLocations;
	public List<GameObject> leaks;
	public enum State {
		EMPTY, FLOODED
	}
	public State state;
	public GameObject flood;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsFlooding() {
		return state == State.FLOODED;
	}

	public void Flood() {
		state = State.FLOODED;
		flood.SetActive(true);
	}
}
