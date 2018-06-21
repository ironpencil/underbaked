using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePile : ProgressiveInteractableImpl, Producer {
    public void OnProduce(GameObject interactor, GameObject product, Interaction interaction)
    {
        Carrier carrier = interactor.GetComponent<Carrier>();
        if (carrier != null) {
            Carryable carryable = product.GetComponent<Carryable>();
            carrier.PickUp(carryable);
        }
    }
}
