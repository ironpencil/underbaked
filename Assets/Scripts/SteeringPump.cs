﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringPump : InteractableBehavior {
	public ShipManager.MoveDirection direction;
	public ShipManager ship;

	public virtual void OnInteract(GameObject interactor, Interaction interaction)
    {
        ship.ChangeHeading(direction);
    }
}
