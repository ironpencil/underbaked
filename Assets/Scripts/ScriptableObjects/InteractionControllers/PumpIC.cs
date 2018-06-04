using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PumpIC", menuName = "InteractionControllers/Pump")]
public class PumpIC : InteractionController
{
    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction)
    {
        PumpStation station = target.GetComponent<PumpStation>();
        station.Push();
    }
}
