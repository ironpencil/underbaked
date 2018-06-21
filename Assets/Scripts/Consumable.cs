using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour {
	public ConsumableType consumableType;

	public float GetEnergyValue() {
		return consumableType.energyValue;
	}
}
