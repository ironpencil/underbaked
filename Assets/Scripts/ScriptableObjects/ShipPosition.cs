using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ship/ShipPosition")]
public class ShipPosition : ScriptableObject {
	public RowId row;
	public RowId nextRow;
	public int step;
}
