using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SubManager : MonoBehaviour {
	public Stage stage;
	public SubPosition position;
	public ShipStats stats;
	public Engine engine;
	public RoomManager roomManager;
	private float nextStep;
	public GameEvent missionEndEvent;
	public List<Cargo> cargo;
	public List<Character> characters;
	public List<Transform> availableCargoPositions;
	public List<Transform> availableSpawnPositions;
	public Respawner respawner;
	public GameState gameState;
	public enum MoveDirection {
		STRAIGHT, LEFT, RIGHT
	}
	public MoveDirection heading = MoveDirection.STRAIGHT;
	public bool printStage = false;

	// Use this for initialization
	void Start () {
		nextStep = Time.time + engine.GetStepFreq();
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

	public void AddCargo(GameObject cargo)
	{
		Assert.IsNotNull(cargo);
		Assert.IsTrue(availableCargoPositions.Count > 0, "No more available cargo positions");

		int i = Random.Range(0, availableCargoPositions.Count - 1);
		cargo.transform.SetParent(availableCargoPositions[i], false);
		availableCargoPositions.RemoveAt(i);
		this.cargo.Add(cargo.GetComponent<Cargo>());
	}

	public void AddCharacter(GameObject charGo)
	{
		Assert.IsNotNull(charGo);
		Assert.IsTrue(availableSpawnPositions.Count > 0, "No more available spawn positions");

		int i = Random.Range(0, availableSpawnPositions.Count - 1);
		charGo.transform.SetParent(availableSpawnPositions[i], false);
		availableSpawnPositions.RemoveAt(i);
		Character character = charGo.GetComponent<Character>();
		this.characters.Add(character);
		character.respawner = respawner;
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
