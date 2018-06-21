using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : InteractableBehavior {
    private Carrier carrier;
    public override void OnInteract(GameObject interactor, Interaction interaction)
    {
        carrier = interactor.GetComponent<Carrier>();

        if (carrier.heldObject != null) {
            carrier.Drop();
        }
        carrier.PickUp(this);
    }
    public Carrier GetCarrier() {
        return carrier;
    }
}