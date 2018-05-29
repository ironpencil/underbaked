using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PumpInteractionController", menuName = "InteractionControllers/Pump")]
public class PumpInteractionController : InteractionController
{
    public List<Interaction> pumpInteractions;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        PumpStation station = target.GetComponent<PumpStation>();
        
        if (pumpInteractions.Contains(interaction))
        {
            station.Push();
        }
    }
}
