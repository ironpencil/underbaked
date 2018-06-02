using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SteeringInteractionController", menuName = "InteractionControllers/Steering")]
public class SteeringInteractionController : InteractionController {
	public List<Interaction> operateInteractions;
	public StageManager stage;
	public MoveSignal moveSignal;
	public enum MoveSignal {
		LEFT, RIGHT
	}
	public override void Interact(GameObject target, GameObject interactor, Interaction interaction) {
		if (operateInteractions.Contains(interaction)) {
			if (moveSignal == MoveSignal.LEFT) {
				stage.MoveLeft();
			} else {
				stage.MoveRight();
			}
		}
	}
}
