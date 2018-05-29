using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	public Stage stage;
	public ShipPosition position;
	public ShipStats stats;
	public RoomManager roomManager;
	private float nextStep;

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
			stage.PrintState(position.row, position.step);
		}
	}

	void TakeStep() {
		AdjustShipPosition();

		if (IsShipColliding()) {
			Debug.Log("Struck a " + GetShipStep().hazard.name);
			DamageShip(GetShipStep().hazard);
		} else {
			Debug.Log("Open seas");
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
