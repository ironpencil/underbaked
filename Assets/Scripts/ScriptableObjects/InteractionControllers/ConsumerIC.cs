using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumerIC : InteractionController
{
    public List<Consumable> acceptedTypes;

    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction)
    {
        Carrier carrier = interactor.GetComponent<Carrier>();

        if (carrier != null && carrier.heldObject != null) {
            Consumable consumable = carrier.heldObject.GetComponent<Consumable>();

            if (consumable != null && acceptedTypes.Contains(consumable)) {
                OnConsume(carrier, consumable, interaction);
                carrier.Drop();
                Destroy(carrier.heldObject.gameObject);
            }
        }
    }

    public abstract void OnConsume(Carrier carrier, Consumable consumable, Interaction interaction);
}
