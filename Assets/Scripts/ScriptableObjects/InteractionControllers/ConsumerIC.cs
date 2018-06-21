using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Consumer")]
public class ConsumerIC : InteractionController
{
    public List<Consumable> acceptedConsumables;

    public override void Interact(GameObject interactor, Interaction interaction)
    {
        if (acceptedInteractions.Contains(interaction)) {
            Feeder feeder = interactor.GetComponent<Feeder>();

            if (feeder != null) {
                Consumable consumable = feeder.GetConsumable();

                if (consumable != null && acceptedConsumables.Contains(consumable)) {
                    foreach (Interactable i in subscribers) {
                        i.OnInteract(interactor, interaction);
                    }
                    Consume(feeder, consumable, interaction);
                }
            }
        }
    }

    public void Consume(Feeder feeder, Consumable consumable, Interaction interaction) {
        foreach (Interactable i in subscribers) {
            if (i is Consumer) {
                ((Consumer)i).OnConsume(feeder.gameObject, consumable, interaction);
            }
        }
        feeder.Feed();
    }
}
