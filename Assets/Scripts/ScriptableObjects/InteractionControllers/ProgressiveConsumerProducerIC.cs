using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Progressive Consumer Producer")]
public class ProgressiveConsumerProducerIC : ProgressiveIC {
    protected Feeder feeder;
    protected Consumable consumable;
    public List<ConsumableType> consumableTypes;
    public GameObject productPrefab;
 
    public override void OnBegin(GameObject interactor, Interaction interaction) {
        feeder = interactor.GetComponent<Feeder>();

        if (feeder == null) {
            failed = true;
            return;
        }

        consumable = feeder.GetConsumable();

        if (consumable == null 
		|| !consumableTypes.Contains(consumable.consumableType)) {
            failed = true;
            return;
        }

        // If we're holding an object, and there is already a job in progress, fail
        if (progression != null && !resetProgress) {
            failed = true;
            return;
        }

        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnBegin(interactor, interaction);
            }
        }

		foreach (Interactable i in subscribers) {
            if (i is Consumer) {
                ((Consumer)i).OnConsume(interactor, consumable, interaction);
            }
        }
        feeder.Feed();
    }

    public override void OnFinish(GameObject interactor, Interaction interaction) {
        GameObject product = Instantiate(productPrefab, interactor.transform);

        foreach (Interactable i in subscribers) {
            if (i is Producer) {
                ((Producer)i).OnProduce(interactor, product, interaction);
            }
        }
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnFinish(interactor, interaction);
            }
        }
    }
}
