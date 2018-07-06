using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vulnerable : MonoBehaviour {
    public List<StatusEffect> statusEffects;
    public List<HazardType> immunities;

    void Start() {
        if (statusEffects == null) statusEffects = new List<StatusEffect>();
        if (immunities == null) immunities = new List<HazardType>();
    }

    public bool IsVulnerable(HazardType vulnerability) {
        return !immunities.Contains(vulnerability);
    }

    public void AddEffect(StatusEffect statusEffect) {
        Debug.Log("Adding effect to vulnerable - no type");
        statusEffects.Add(statusEffect);
        statusEffect.Begin();
    }

    public void AddEffect(StatusEffect statusEffect, HazardType hazardType) {
        Debug.Log("Adding effect to vulnerable");
        if (IsVulnerable(hazardType)) {
            statusEffect.Begin();
            statusEffects.Add(statusEffect);
        }
    }

    public void RemoveEffect(StatusEffect statusEffect) {
        statusEffects.Remove(statusEffect);
        statusEffect.End();
    }

    public void RemoveAllEffects() {
        foreach(StatusEffect statusEffect in statusEffects) {
            statusEffect.End();
        }
        statusEffects.Clear();
    }

    void Update() {
        foreach (StatusEffect se in statusEffects) {
            se.Update(Time.deltaTime);
        }
    }
}
