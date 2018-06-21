using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Progressive Consumer")]
public class ProgressiveConsumerIC : ProgressiveIC {
    private Carrier carrier;
    private Consumable consumable;
    public List<ConsumableType> consumableTypes;

    public override void OnBegin(GameObject interactor, Interaction interaction) {
        carrier = interactor.GetComponent<Carrier>();

        if (carrier == null || carrier.heldObject == null) {
            failed = true;
            return;
        }

        consumable = carrier.heldObject.GetComponent<Consumable>();

        if (consumable == null) {
            failed = true;
            return;
        }

        if (!consumableTypes.Contains(consumable.consumableType)) {
            failed = true;
            return;
        }

        // If there is already a job in progress, fail
        if (progression != null && !resetProgress) {
            failed = true;
            return;
        }

        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnBegin(interactor, interaction);
            }
        }
    }

    public override void OnFinish(GameObject interactor, Interaction interaction) {
        ConsumeObject(carrier);
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnFinish(interactor, interaction);
            }
        }
    }

    private void ConsumeObject(Carrier carrier) {
        GameObject consumable = carrier.heldObject.gameObject;
        carrier.Drop();
        Destroy(consumable);
    }
}
