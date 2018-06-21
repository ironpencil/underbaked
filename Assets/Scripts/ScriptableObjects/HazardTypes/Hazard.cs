using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour {
    public List<Vulnerability> vulnerables;
    public HazardType hazardType;

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
				vulnerables.Remove(vulnerable);
			}
		}
	}

	private void AddVulnerable(Vulnerability vulnerable) {
		if (vulnerable != null) {
			if (!vulnerables.Contains(vulnerable)) {
				vulnerables.Add(vulnerable);
			}
		}
	}

    public abstract void CheckForExposures();
}
