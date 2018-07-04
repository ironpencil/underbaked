using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : Hazard
{
    SpriteRenderer sprite;

    public float waterValue = 0.0f;
    public int maxWaterVolume = 100;
    public int floodThreshold = 50;

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
        CheckForExposures();
    }

    public override void CheckForExposures()
    {
        for (int i = vulnerables.Count - 1; i >= 0; i--) {
    		Vulnerability vulnerable = vulnerables[i];
    		if (FloodLevel > 0 && vulnerable != null) {
    			vulnerable.Expose(hazardType);
    		}
    	}
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
        }
        else
        {
            FloodLevel = 0;
        }
    }
}