using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPosition {
	public RowConfig row;
	public RowConfig nextRow;
	// The ship should start with one space behind them
	public int step = 1;
}
