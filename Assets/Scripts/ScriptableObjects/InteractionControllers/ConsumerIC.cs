using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Consumer")]
public class ConsumerIC : InteractionController
{
    public List<Consumable> acceptedTypes;

    public override void Interact(GameObject interactor, Interaction interaction)
    {
        Interact(interactor, interaction);

        Carrier carrier = interactor.GetComponent<Carrier>();

        if (carrier != null && carrier.heldObject != null) {
            Consumable consumable = carrier.heldObject.GetComponent<Consumable>();

            if (consumable != null && acceptedTypes.Contains(consumable)) {
                Consume(carrier, consumable, interaction);
                carrier.Drop();
                Destroy(carrier.heldObject.gameObject);
            }
        }
    }

    public void Consume(Carrier carrier, Consumable consumable, Interaction interaction) {
        foreach (Interactable i in subscribers) {
            if (i is Consumer) {
                ((Consumer)i).OnConsume(carrier, consumable, interaction);
            }
        }
    }
}
