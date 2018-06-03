using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {
	public Stage stage;
	public ShipPosition position;
	public ShipStats stats;
	public RoomManager roomManager;
	private float nextStep;
	public bool printStage = false;
	public enum MoveDirection {
		STRAIGHT, LEFT, RIGHT
	}
	public MoveDirection heading = MoveDirection.STRAIGHT;

	// Use this for initialization
	void Start () {
		nextStep = Time.time + stats.stepFrequency;
		stage.Build();
		position.row = stage.startRow;
		position.nextRow = stage.startRow;
		position.step = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextStep) {
			TakeStep();

			if (printStage) {
				stage.PrintState(position.row, position.step);
			}
		}
	}

	public void ChangeHeading(MoveDirection direction) {
		RowConfig checkRow = null;

		if (direction == MoveDirection.LEFT) {
			checkRow = stage.GetRowLeftNeighbor(position.nextRow);
		} else if (direction == MoveDirection.RIGHT) {
			checkRow = stage.GetRowRightNeighbor(position.nextRow);
		}

		if (checkRow != null) {
			// If the distance between our current row, and the next potential row
			// is less than or equal to our maximum movement, go ahead and move
			if (stage.GetDistanceBeteenRows(position.row, checkRow) <= stats.maxRowMovement) {
				position.nextRow = checkRow;
				SetHeading();
			}
		}
	}

	public void SetHeading() {
		heading = stage.GetHeading(position.row, position.nextRow);
	}

	void TakeStep() {
		AdjustShipPosition();

		if (IsShipColliding()) {
			DamageShip(GetShipStep().hazard);
		}
		
		nextStep = Time.time + stats.stepFrequency;
	}

	void AdjustShipPosition() {
		position.row = position.nextRow;
		position.step++;
	}

	bool IsShipColliding() {
		return GetShipStep().hazard != null;
	}

	void DamageShip(ShipHazard hazard) {
		for (int i = 0; i < hazard.leaksCaused; i++) {
			roomManager.SpringRandomLeak();
		}
	}

	Step GetShipStep() {
		return stage.GetStep(position.row, position.step);
	}
}
