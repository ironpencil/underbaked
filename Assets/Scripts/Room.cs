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

	public bool IsFlooded() {
		return state == State.FLOODED;
	}

	public bool IsLeaking() {
		return leaks.Count > 0;
	}

	public void Flood() {
		state = State.FLOODED;
		flood.SetActive(true);
	}

	public void CleanUpLeaks() {
		for (int i = leaks.Count - 1; i >= 0; i--) {
			Leak leak = leaks[i].GetComponent<Leak>();
			if (leak.IsFixed()) {
				availLeakLocations.Add(leak.transform.parent);
				leaks.RemoveAt(i);
				Destroy(leak.gameObject);
			}
		}
	}
}
