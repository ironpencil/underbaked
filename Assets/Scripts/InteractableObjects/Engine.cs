using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : InteractableBehavior, Consumer {
    public Color overColor;
    public Color underColor;
    public Color poweredColor;
    public EngineStats engineStats;
    public float currentEnergy;
	public ENGINE_STATE state;
	public enum ENGINE_STATE {
		UNDERPOWERED, POWERED, OVERPOWERED
	}

    public float GetStepFreq() {
		switch (state)
		{
			case ENGINE_STATE.UNDERPOWERED:
				return engineStats.underStepFreq;
			case ENGINE_STATE.POWERED:
				return engineStats.baseStepFreq;
			case ENGINE_STATE.OVERPOWERED:
				return engineStats.overStepFreq;
			default:
				return engineStats.baseStepFreq;
		}
	}

    public void AddEnergy(float energyValue)
    {
        currentEnergy += energyValue;
		UpdateEnergyState();
    }

	public void BurnEnergy(float deltaTime)
	{
		if (currentEnergy <= 0) {
			currentEnergy = 0;
		} else {
			currentEnergy -= engineStats.fuelBurnFreq * deltaTime;
		}

		UpdateEnergyState();
	}

	private void UpdateEnergyState()
	{
		if (currentEnergy > engineStats.overMinEnergy) {
			state = ENGINE_STATE.OVERPOWERED;
		} else if (currentEnergy > engineStats.baseMinEnergy) {
			state = ENGINE_STATE.POWERED;
		} else {
			state = ENGINE_STATE.UNDERPOWERED;
		}
	}

    public void OnConsume(GameObject interactor, Consumable consumable, Interaction interaction)
    {
        AddEnergy(consumable.GetEnergyValue());
    }
}
