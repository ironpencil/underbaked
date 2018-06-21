using UnityEngine;

public class ProgressiveProducerInteractableBehavior : ProgressiveInteractableImpl, Producer
{
    public void OnProduce(GameObject interactor, GameObject product, Interaction interaction)
    {
        Carrier carrier = interactor.GetComponent<Carrier>();
        Carryable carryable = product.GetComponent<Carryable>();
        if (carrier != null && carryable != null) {
            carrier.PickUp(carryable);
        }
    }
}