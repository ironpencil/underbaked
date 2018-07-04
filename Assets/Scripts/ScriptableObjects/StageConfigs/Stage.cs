using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName ="Stage/Stage")]
public class Stage : ScriptableObject {
	public int stepCount;
	public List<ShipHazardChance> hazardChances;
	public List<RowConfig> rowConfigs;
	public Dictionary<RowConfig, List<Step>> rows;
	public RowConfig startRow;
	public List<CargoCount> cargoCounts;
	public List<MissionTimeMultiplier> missionTimeMultipliers;

	public void Build() {
		rows = new Dictionary<RowConfig, List<Step>>();
		foreach (RowConfig rowConfig in rowConfigs) {
			List<Step> row = new List<Step>();
			for (int i = 0; i < stepCount; i++) {
				if (i <= 2) {
					row.Add(new Step());
				} else {
					row.Add(GenerateStep());
				}
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

		return index;
	}

	public Step GenerateStep() {
		Step step = new Step();

		foreach (ShipHazardChance hazardChance in hazardChances) {
			if (hazardChance.chance > Random.value) {
				step.hazard = hazardChance.hazard;
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

	public Step GetStep(int row, int stepId) {
		return GetStep(rowConfigs[row], stepId);
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

	public SubManager.MoveDirection GetHeading(RowConfig from, RowConfig to) {
		if (from == to) return SubManager.MoveDirection.STRAIGHT;
		if (GetRowIndex(from) > GetRowIndex(to)) {
			return SubManager.MoveDirection.LEFT;
		} else {
			return SubManager.MoveDirection.RIGHT;
		}
	}

	public float GetMultiplier(float time) {
		foreach (MissionTimeMultiplier mtm in missionTimeMultipliers)
		{
			if (time < mtm.time) return mtm.multiplier;
		}
		return 1;
	}
}
