using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : InteractableBehavior {
    public Carrier carrier;
    public override void OnInteract(GameObject interactor, Interaction interaction)
    {
        carrier = interactor.GetComponent<Carrier>();
        if (carrier != null) {
            carrier.PickUp(this);
        }
    }
}