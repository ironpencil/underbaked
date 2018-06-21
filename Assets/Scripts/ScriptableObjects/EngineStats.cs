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
}
