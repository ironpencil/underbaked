using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ship/EngineStats")]
public class EngineStats : ScriptableObject {
	public float underStepFreq;
	public float baseMinEnergy;
	public float baseStepFreq;
	public float overMinEnergy;
	public float overStepFreq;
	public float fuelBurnFreq;
	public float currentEnergy;
	public ENGINE_STATE state;
	public enum ENGINE_STATE {
		UNDERPOWERED, POWERED, OVERPOWERED
	}
	public float GetStepFreq() {
		Debug.Log("Current Energy: " + currentEnergy);
		switch (state)
		{
			case ENGINE_STATE.UNDERPOWERED:
				return underStepFreq;
			case ENGINE_STATE.POWERED:
				return baseStepFreq;
			case ENGINE_STATE.OVERPOWERED:
				return overStepFreq;
			default:
				return baseStepFreq;
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
			currentEnergy -= fuelBurnFreq * deltaTime;
		}

		UpdateEnergyState();
	}

	private void UpdateEnergyState()
	{
		if (currentEnergy > overMinEnergy) {
			state = ENGINE_STATE.OVERPOWERED;
		} else if (currentEnergy > baseMinEnergy) {
			state = ENGINE_STATE.POWERED;
		} else {
			state = ENGINE_STATE.UNDERPOWERED;
		}
	}
}
