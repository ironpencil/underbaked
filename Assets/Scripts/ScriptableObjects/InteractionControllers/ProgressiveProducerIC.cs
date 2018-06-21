using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Progressive Producer")]
public abstract class ProgressiveProducerIC : ProgressiveIC {
    private Carrier carrier;
    public Carryable product;

    public override void OnBegin(GameObject interactor, Interaction interaction) {
        carrier = interactor.GetComponent<Carrier>();

        if (carrier == null || product == null) {
            failed = true;
            return;
        }

        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnBegin(interactor, interaction);
            }
        }
    }

    public sealed override void OnStart(GameObject interactor, Interaction interaction) {
        if (carrier.heldObject != null) {
            failed = true;
            return;
        }
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnStart(interactor, interaction, progression);
            }
        }
    }
    // TODO Not sure if it should be the responsibility of this to actually make the "pick up" request
    public sealed override void OnFinish(GameObject interactor, Interaction interaction) {
        Carryable carryable = Instantiate(product, carrier.transform);
        foreach (Interactable i in subscribers) {
            if (i is Producer) {
                ((Producer)i).OnProduce(interactor, carryable, interaction);
            }
        }
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnFinish(interactor, interaction);
            }
        }
    }
}
