using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : InteractableBehavior, Consumer {
	public Sprite image;
    public Color overColor;
    public Color underColor;
    public Color poweredColor;
    public EngineStats stats;

	public void BurnEnergy(float deltaTime) {
        stats.BurnEnergy(deltaTime);
    }

    public float GetStepFreq() {
        return stats.GetStepFreq();
    }

    public void OnConsume(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        stats.AddEnergy(consumable.consumableType.energyValue);
    }
}
