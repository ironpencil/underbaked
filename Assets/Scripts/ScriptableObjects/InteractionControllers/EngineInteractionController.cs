using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EngineInteractionController", menuName = "InteractionControllers/Engine")]
public class EngineInteractionController : InteractionController
{
    public List<Interaction> fuelInteractions;
    public List<FuelType> expectedTypes;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        if (fuelInteractions.Contains(interaction))
        {
            Carrier carrier = interactor.GetComponent<Carrier>();

            if (carrier != null && carrier.heldObject != null) {
                Fuel fuel = carrier.heldObject.GetComponent<Fuel>();

                if (fuel != null && expectedTypes.Contains(fuel.fuelType)) {
                    carrier.Drop();
                    Destroy(fuel.gameObject);
                }
            }
        }
    }
}
