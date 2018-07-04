using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour {
	public CargoStats stats;

	public int GetValue()
	{
		return stats.value;
	}
}
