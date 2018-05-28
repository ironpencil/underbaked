using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	public Connection[] connections;
	public List<Transform> availLeakLocations;
	public List<GameObject> leaks;
	public List<Vulnerability> vulnerables;
	public enum State {
		EMPTY, FLOODED, DRAINING
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

	/* The RoomManager will come clean this up. We would like to
	   consider handling this a different way.
	*/
	public void StartDraining() {
		state = State.DRAINING;
	}

	public bool IsDraining() {
		return state == State.DRAINING;
	}
	
	public void Drain() {
		state = State.EMPTY;
		flood.SetActive(false);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		Carrier carrier = other.GetComponent<Carrier>();
		
		if (carrier != null && carrier.heldObject != null) {
			AddVulnerable(carrier.heldObject.GetComponent<Vulnerability>());
		}
		AddVulnerable(other.GetComponent<Vulnerability>());
	}

	public void OnTriggerExit2D(Collider2D other) {
		Carrier carrier = other.GetComponent<Carrier>();
		
		if (carrier != null && carrier.heldObject != null) {
			RemoveVulnerable(carrier.heldObject.GetComponent<Vulnerability>());
		}
		RemoveVulnerable(other.GetComponent<Vulnerability>());
	}

	private void RemoveVulnerable(Vulnerability vulnerable) {
		if (vulnerable != null) {
			if (vulnerables.Contains(vulnerable)) {
				Debug.Log("Removing Vul: " + vulnerable.transform.name);
				vulnerables.Remove(vulnerable);
			}
		}
	}

	private void AddVulnerable(Vulnerability vulnerable) {
		if (vulnerable != null) {
			if (!vulnerables.Contains(vulnerable)) {
				Debug.Log("Adding Vul: " + vulnerable.transform.name);
				vulnerables.Add(vulnerable);
			}
		}
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

	public void CheckForExposures() {
		for (int i = vulnerables.Count - 1; i >= 0; i--) {
			Vulnerability vulnerable = vulnerables[i].GetComponent<Vulnerability>();
			if (IsFlooded()) {
				vulnerable.Expose(flood.GetComponent<Flood>().hazardType);
			}
		}
	}
}
