using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour {
    public List<Vulnerability> vulnerables;
    public HazardType hazardType;

	public void RemoveVulnerable(Vulnerability vulnerable) {
		if (vulnerable != null) {
			if (vulnerables.Contains(vulnerable)) {
				EndExposure(vulnerable);
				vulnerables.Remove(vulnerable);
			}
		}
	}

	public void AddVulnerable(Vulnerability vulnerable) {
		if (vulnerable != null) {
			if (!vulnerables.Contains(vulnerable)) {
				vulnerables.Add(vulnerable);
			}
		}
	}

    public abstract void CheckForExposures();

	public abstract void EndExposure(Vulnerability vulnerable);
}
