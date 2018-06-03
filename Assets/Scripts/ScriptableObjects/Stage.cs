using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName ="Stage/Stage")]
public class Stage : ScriptableObject {
	public int stepCount;
	public List<ShipHazard> hazards;
	public List<RowConfig> rowConfigs;
	public Dictionary<RowConfig, List<Step>> rows;
	public RowConfig startRow;

	public void Build() {
		rows = new Dictionary<RowConfig, List<Step>>();
		foreach (RowConfig rowConfig in rowConfigs) {
			List<Step> row = new List<Step>();
			for (int i = 0; i < stepCount; i++) {
				row.Add(GenerateStep());
			}
			rows.Add(rowConfig, row);
		}
	}

	public RowConfig GetRowLeftNeighbor(RowConfig row) {
		int rowIdx = GetRowIndex(row);
		if (rowIdx > 0) {
			return rowConfigs[rowIdx - 1];
		}
		return null;
	}

	public RowConfig GetRowRightNeighbor(RowConfig row) {
		int rowIdx = GetRowIndex(row);
		if (rowIdx < rowConfigs.Count - 1) {
			return rowConfigs[rowIdx + 1];
		}
		return null;
	}

	public int GetDistanceBeteenRows(RowConfig rowA, RowConfig rowB) {
		int distance = -1;
		int rowAIdx = GetRowIndex(rowA);
		int rowBIdx = GetRowIndex(rowB);

		if (rowAIdx >= 0 && rowBIdx >= 0) {
			distance = Mathf.Abs(rowAIdx - rowBIdx);
		}
		return distance;
	}

	public int GetRowIndex(RowConfig row) {
		int index = -1;
		for (int i = 0; i < rowConfigs.Count; i++) {
			if (rowConfigs[i] == row) return i;
		}

		Debug.Log("Return Row Index: " + index);

		return index;
	}

	public Step GenerateStep() {
		Step step = new Step();

		foreach (ShipHazard hazard in hazards) {
			if (hazard.chance > Random.value) {
				step.hazard = hazard;
				break;
			}
		}

		return step;
	}

	public Step GetStep(RowConfig rowConfig, int stepId) {
		Step step = null;
		if (rows.ContainsKey(rowConfig)) {
			List<Step> row = rows[rowConfig];
			if (stepId < row.Count) {
				step = row[stepId];
			}
		}

		return step;
	}

	public void PrintState(RowConfig shipRowConfig, int shipStep) {
		foreach (RowConfig rowConfig in rowConfigs) {
			StringBuilder sb = new StringBuilder();
			List<Step> steps = rows[rowConfig];
			for (int stepId = 0; stepId < steps.Count; stepId++) {
				Step step = steps[stepId];
				if (shipRowConfig == rowConfig && shipStep == stepId) {
					sb.Append("X");
				} else if (step.hazard != null) {
					sb.Append(step.hazard.letter);
				} else {
					sb.Append("O");
				}
			}
			Debug.Log(sb.ToString());
		}
	}

	public ShipManager.MoveDirection GetHeading(RowConfig from, RowConfig to) {
		if (from == to) return ShipManager.MoveDirection.STRAIGHT;
		if (GetRowIndex(from) > GetRowIndex(to)) {
			return ShipManager.MoveDirection.LEFT;
		} else {
			return ShipManager.MoveDirection.RIGHT;
		}
	}
}
