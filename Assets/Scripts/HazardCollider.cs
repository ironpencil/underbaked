using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollider : MonoBehaviour {
	private Hazard hazard;
	void Start()
	{
		hazard = GetComponentInParent<Hazard>();
	}

	public void OnTriggerEnter2D(Collider2D other) {
		Carrier carrier = other.GetComponent<Carrier>();
		if (carrier != null && carrier.heldObject != null) {
			AddVulnerables(carrier.heldObject.gameObject);
		}
		AddVulnerables(other.gameObject);
	}

	public void OnTriggerExit2D(Collider2D other) {
		Carrier carrier = other.GetComponent<Carrier>();
		if (carrier != null && carrier.heldObject != null) {
			RemoveVulnerables(carrier.heldObject.gameObject);
		}
		RemoveVulnerables(other.gameObject);
	}
	
	void AddVulnerables(GameObject go) {
		Vulnerable[] vulns = go.GetComponents<Vulnerable>();
		if (vulns != null) {
			foreach (Vulnerable vuln in vulns) {
				hazard.AddVulnerable(vuln);
			}
		}
	}

	void RemoveVulnerables(GameObject go) {
		Vulnerable[] vulns = go.GetComponents<Vulnerable>();
		if (vulns != null) {
			foreach (Vulnerable vuln in vulns) {
				hazard.RemoveVulnerable(vuln);
			}
		}
	}
}
