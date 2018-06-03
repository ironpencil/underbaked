using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ship/ShipPosition")]
public class ShipPosition : ScriptableObject {
	public RowConfig row;
	public RowConfig nextRow;
	// The ship should start with one space behind them
	public int step = 1;
}
