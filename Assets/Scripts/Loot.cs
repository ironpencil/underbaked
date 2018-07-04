using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
	public LootStats stats;

	public int GetValue()
	{
		return stats.value;
	}
}
