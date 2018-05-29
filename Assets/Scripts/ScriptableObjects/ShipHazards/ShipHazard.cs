using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ship/Hazard")]
public class ShipHazard : ScriptableObject {
	public int leaksCaused;
	public float chance;
	public string letter;
}
