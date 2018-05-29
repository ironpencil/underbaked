using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	public Stage stage;
	public ShipPosition position;
	public ShipStats stats;
	public RoomManager roomManager;
	private float nextStep;
	public bool printStage = false;

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
