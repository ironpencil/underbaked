using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgressiveProducerIC : ProgressiveIC {
    private Carrier carrier;
    public Carryable product;

    public override void OnBegin(GameObject interactor, GameObject target, Interaction interaction) {
        carrier = interactor.GetComponent<Carrier>();

        if (carrier == null || product == null) {
            failed = true;
            return;
        }

        OnBegin(carrier, this, interaction);
    }

    public sealed override void OnStart(GameObject interactor, GameObject target, Interaction interaction) {
        if (carrier.heldObject != null) {
            failed = true;
            return;
        }
        OnStart(carrier, this, interaction);
    }

    public sealed override void OnUpdate(GameObject interactor, GameObject target, Interaction interaction) {
        OnUpdate(carrier, this, interaction);
    }

    public sealed override void OnStop(GameObject interactor, GameObject target, Interaction interaction) {
        OnStop(carrier, this, interaction);
    }

    public sealed override void OnFinish(GameObject interactor, GameObject target, Interaction interaction) {
        carrier.PickUp(Instantiate(product, carrier.transform));
        Debug.Log("Starting ProgressiveProducer.OnFinish coroutine");
        OnFinish(carrier, this, interaction);
        Debug.Log("Completed ProgressiveProducer.OnFinish coroutine");
    }

    public abstract void OnBegin(Carrier carrier, ProgressiveProducerIC producer, Interaction interaction);

    public abstract void OnStart(Carrier carrier, ProgressiveProducerIC producer, Interaction interaction);

    public abstract void OnUpdate(Carrier carrier, ProgressiveProducerIC producer, Interaction interaction);

    public abstract void OnStop(Carrier carrier, ProgressiveProducerIC producer, Interaction interaction);

    public abstract void OnFinish(Carrier carrier, ProgressiveProducerIC producer, Interaction interaction);
}
