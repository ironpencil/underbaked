using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringPump : InteractableBehavior {
	public SubManager.MoveDirection direction;
	public SubManager sub;

	public override void OnInteract(GameObject interactor, Interaction interaction)
    {
        sub.ChangeHeading(direction);
    }
}
