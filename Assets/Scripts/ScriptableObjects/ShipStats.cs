using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ship/ShipStats")]
public class ShipStats : ScriptableObject {
	public float stepFrequency;
	public float maxRowMovement;
	public int maxParascopeDistance;
	public int minParascopeDistance;
}
