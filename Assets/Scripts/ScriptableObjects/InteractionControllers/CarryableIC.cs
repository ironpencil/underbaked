﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarryableIC", menuName = "InteractionControllers/Carryable")]
public class CarryableIC : InteractionController
{
    public List<Interaction> pickupInteractions;

    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction)
    {
        Carryable carryable = target.GetComponent<Carryable>();
        Carrier carrier = interactor.GetComponent<Carrier>();
        
        if (carrier != null 
        && carryable != null 
        && pickupInteractions.Contains(interaction))
        {
            if (carrier.heldObject != null) {
                carrier.Drop();
                OnDrop(carrier.heldObject, carrier, interaction);
            }
            carrier.PickUp(carryable);
            OnPickup(carryable, carrier, interaction);
        }
    }

    public void OnPickup(Carryable carryable, Carrier carrier, Interaction interaction) {

    }

    public void OnDrop(Carryable carryable, Carrier carrier, Interaction interaction) {
        
    }
}
