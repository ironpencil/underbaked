using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgressiveConsumerProducerIC : ProgressiveIC {
    protected Carrier carrier;
    protected Workbench workbench;
    protected Consumable consumable;
    public List<ConsumableType> consumableTypes;
    public Carryable product;
 
    public override void OnBegin(GameObject interactor, GameObject target, Interaction interaction) {
        carrier = interactor.GetComponent<Carrier>();
        workbench = target.GetComponent<Workbench>();
        
        if (carrier == null || carrier.heldObject == null || product == null) {
            failed = true;
        }

        consumable = carrier.heldObject.GetComponent<Consumable>();

        if (consumable == null) {
            failed = true;
        }

        if (!consumableTypes.Contains(consumable.consumableType)) {
            failed = true;
        }

        // If we're holding an object, and there is already a job in progress, fail
        if (progression != null && !resetProgress) {
            failed = true;
        }

        OnBegin(carrier, consumable, interaction);

        ConsumeObject(carrier);
    }

    private void ConsumeObject(Carrier carrier) {
        GameObject consumable = carrier.heldObject.gameObject;
        carrier.Drop();
        Destroy(consumable);
    }

    public sealed override void OnStart(GameObject interactor, GameObject target, Interaction interaction) {
        OnStart(carrier, consumable, interaction);
    }

    public sealed override void OnUpdate(GameObject interactor, GameObject target, Interaction interaction) {
        OnUpdate(carrier, consumable, interaction);
    }

    public sealed override void OnStop(GameObject interactor, GameObject target, Interaction interaction) {
        OnStop(carrier, consumable, interaction);
    }

    public sealed override void OnFinish(GameObject interactor, GameObject target, Interaction interaction) {
        carrier.PickUp(Instantiate(product, carrier.transform));
        OnFinish(carrier, consumable, interaction);
    }

    public abstract void OnBegin(Carrier carrier, Consumable consumable, Interaction interaction);

    public abstract void OnStart(Carrier carrier, Consumable consumable, Interaction interaction);

    public abstract void OnUpdate(Carrier carrier, Consumable consumable, Interaction interaction);

    public abstract void OnStop(Carrier carrier, Consumable consumable, Interaction interaction);

    public abstract void OnFinish(Carrier carrier, Consumable consumable, Interaction interaction);
}
