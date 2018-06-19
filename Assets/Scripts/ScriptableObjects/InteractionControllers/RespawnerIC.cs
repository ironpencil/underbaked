using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RespawnerIC", menuName = "InteractionControllers/Respawner")]
public class RespawnerIC : InteractionController
{
    public List<Interaction> repairInteractions;
    public List<Interaction> breakInteractions;

    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction)
    {
        Respawner respawner = target.GetComponent<Respawner>();

        if (breakInteractions.Contains(interaction))
        {
            respawner.Damage();
        }
        
        if (repairInteractions.Contains(interaction))
        {
            respawner.Repair();
        }
    }
}
