using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName ="Stage/Stage")]
public class Stage : ScriptableObject {
	public int stepCount;
	public List<ShipHazard> hazards;
	public List<RowId> rowIds;
	public Dictionary<RowId, List<Step>> rows;
	public RowId startRow;

	public void Build() {
		rows = new Dictionary<RowId, List<Step>>();
		foreach (RowId rowId in rowIds) {
			List<Step> row = new List<Step>();
			for (int i = 0; i < stepCount; i++) {
				row.Add(GenerateStep());
			}
			rows.Add(rowId, row);
		}
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

	public Step GetStep(RowId rowId, int stepId) {
		Step step = null;
		if (rows.ContainsKey(rowId)) {
			List<Step> row = rows[rowId];
			if (stepId < row.Count) {
				step = row[stepId];
			}
		}

		return step;
	}

	public void PrintState(RowId shipRowId, int shipStep) {
		foreach (RowId rowId in rowIds) {
			StringBuilder sb = new StringBuilder();
			List<Step> steps = rows[rowId];
			for (int stepId = 0; stepId < steps.Count; stepId++) {
				Step step = steps[stepId];
				if (shipRowId == rowId && shipStep == stepId) {
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
}
