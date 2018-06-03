using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SteeringInteractionController", menuName = "InteractionControllers/Steering")]
public class SteeringInteractionController : InteractionController {
	public List<Interaction> operateInteractions;
	public ShipManager.MoveDirection direction;

	public override void Interact(GameObject target, GameObject interactor, Interaction interaction) {
		if (operateInteractions.Contains(interaction)) {
			target.GetComponent<ShipManager>().ChangeHeading(direction);
		}
	}
}
