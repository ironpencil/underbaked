using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SteeringIC", menuName = "InteractionControllers/Steering")]
public class SteeringIC : InteractionController {
	public List<Interaction> operateInteractions;
	public ShipManager.MoveDirection direction;

	public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction) {
		target.GetComponent<ShipManager>().ChangeHeading(direction);
	}
}
