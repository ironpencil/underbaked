using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarryableInteractionController", menuName = "InteractionControllers/Carryable")]
public class CarryableInteractionController : InteractionController
{

    public List<Interaction> pickupInteractions;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        Carryable carryable = target.GetComponent<Carryable>();
        Carrier carrier = interactor.GetComponent<Carrier>();
        
        if (carrier != null 
        && carryable != null 
        && pickupInteractions.Contains(interaction))
        {
            carrier.PickUp(carryable);
        }
    }
}
