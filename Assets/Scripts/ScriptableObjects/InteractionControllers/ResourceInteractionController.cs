using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceInteractionController", menuName = "InteractionControllers/Resource")]
public class ResourceInteractionController : InteractionController
{
    public List<Interaction> gatherInteractions;
    public Carryable resourcePrefab;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        Carrier carrier = interactor.GetComponent<Carrier>();
        if (gatherInteractions.Contains(interaction) 
        && carrier != null
        && carrier.heldObject == null)
        {
            carrier.PickUp(Instantiate(resourcePrefab, interactor.transform));
        }
    }
}
