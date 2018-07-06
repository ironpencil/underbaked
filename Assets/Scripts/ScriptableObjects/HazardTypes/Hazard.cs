using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour {
    public HazardType hazardType;
	public Dictionary<Vulnerable, List<StatusEffect>> affectedVulnerables = new Dictionary<Vulnerable, List<StatusEffect>>();

	void Start() {
		
	}

	public virtual void AddVulnerable(Vulnerable vulnerable) {
		affectedVulnerables.Add(vulnerable, new List<StatusEffect>());
	}

	public virtual void AddStatusEffect(Vulnerable vulnerable, StatusEffect statusEffect, HazardType hazardType) {
		affectedVulnerables[vulnerable].Add(statusEffect);
		vulnerable.AddEffect(statusEffect, hazardType);
	}

	public virtual void RemoveVulnerable(Vulnerable vulnerable) {
		RemoveEffects(vulnerable);
		affectedVulnerables.Remove(vulnerable);
	}

	public virtual void RemoveEffects(Vulnerable vulnerable) {
		foreach (StatusEffect statusEffect in affectedVulnerables[vulnerable]) {
			vulnerable.RemoveEffect(statusEffect);
		}
	}
}
