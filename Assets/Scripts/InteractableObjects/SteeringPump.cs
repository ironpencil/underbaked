using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringPump : InteractableBehavior {
	public SubManager.MoveDirection direction;
	public SubManager subManager;

    public override void Start()
    {
        base.Start();
    }

	public override void OnInteract(GameObject interactor, Interaction interaction)
    {
        subManager.ChangeHeading(direction);
    }
}
