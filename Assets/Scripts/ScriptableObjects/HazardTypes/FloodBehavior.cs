using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodBehavior : Hazard
{
    SpriteRenderer sprite;

    public float waterValue = 0.0f;
    public int maxWaterVolume = 100;
    public int floodThreshold = 50;
    public float movementSpeedEffect = .5f;

    [SerializeField]
    int floodLevel = 0;
    public int FloodLevel {
        get { return floodLevel; }
        private set { floodLevel = value; }
    }

    void Start() {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void LateUpdate()
    {
        SetFloodLevel();
        DisplayFlooding();
    }

    public void ChangeWaterValue(float waterValue)
    {
        this.waterValue = Mathf.Clamp(this.waterValue + waterValue, 0, maxWaterVolume);
    }

    private void DisplayFlooding()
    {
        Color newColor = sprite.color;
        newColor.a = waterValue / maxWaterVolume;
        sprite.color = newColor;
    }

    private void SetFloodLevel()
    {
        if (Mathf.RoundToInt(waterValue) >= floodThreshold)
        {
            FloodLevel = 1;
            AffectVulnerables();
        }
        else
        {
            FloodLevel = 0;
        }
    }

    public override void AddVulnerable(Vulnerable vulnerable)
    {
        base.AddVulnerable(vulnerable);
        if (FloodLevel > 0) AddStatusEffectsToVulnerable(vulnerable);
    }

    public void AffectVulnerables() {
        foreach (Vulnerable vulnerable in affectedVulnerables.Keys) {
            AddStatusEffectsToVulnerable(vulnerable);
        }
    }

    private void ClearEffects() {
        foreach (Vulnerable vulnerable in affectedVulnerables.Keys) {
            RemoveEffects(vulnerable);
        }
    }

    private void AddStatusEffectsToVulnerable(Vulnerable vulnerable) {
        Character character = vulnerable.GetComponent<Character>();
        if (character != null) {
            AddStatusEffect(vulnerable, new Drowning(character), hazardType);
            AddStatusEffect(vulnerable, new Slow(character, movementSpeedEffect), hazardType);
        }
    }
}