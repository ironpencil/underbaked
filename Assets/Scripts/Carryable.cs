using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : InteractableBehavior {
    private Carrier carrier;
    public void OnInteract(GameObject target, GameObject interactor, Interaction interaction)
    {
        carrier = interactor.GetComponent<Carrier>();

        if (carrier.heldObject != null) {
            carrier.Drop();
        }
        Carryable carryable = target.GetComponent<Carryable>();
        carrier.PickUp(carryable);
    }
    public Carrier GetCarrier() {
        return carrier;
    }
}