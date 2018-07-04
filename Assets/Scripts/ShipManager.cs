﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {
	public Stage stage;
	public ShipPosition position;
	public ShipStats stats;
	public Engine engine;
	public RoomManager roomManager;
	private float nextStep;
	public GameEvent missionEndEvent;
	public List<Loot> loot;
	public GameState gameState;
	public enum MoveDirection {
		STRAIGHT, LEFT, RIGHT
	}
	public MoveDirection heading = MoveDirection.STRAIGHT;
	public bool printStage = false;

	// Use this for initialization
	void Start () {
		nextStep = Time.time + engine.GetStepFreq();
		stage.Build();
		position.row = stage.startRow;
		position.nextRow = stage.startRow;
		position.step = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState.isInMission) {
			engine.BurnEnergy(Time.deltaTime);

			if (Time.time > nextStep) {
				if (IsOutOfSteps()) {
					missionEndEvent.Raise();
				} else {
					TakeStep();
					if (printStage) {
						stage.PrintState(position.row, position.step);
					}
				}
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
		
		nextStep = Time.time + engine.GetStepFreq();
	}

	void AdjustShipPosition() {
		position.row = position.nextRow;
		position.step++;
	}

	bool IsOutOfSteps() {
		return position.step == stage.stepCount;
	}

	bool IsShipColliding() {
		Step nextStep = GetShipStep();
		return nextStep != null && nextStep.hazard != null;
	}

	void DamageShip(ShipHazard hazard) {
		for (int i = 0; i < hazard.leaksCaused; i++) {
			roomManager.SpringRandomLeak();
		}
	}

	Step GetShipStep() {
		return stage.GetStep(position.row, position.step);
	}

	public int GetRowIndex()
	{
		return stage.GetRowIndex(position.row);
	}

	public int GetNextRowIndex()
	{
		return stage.GetRowIndex(position.nextRow);
	}
}
