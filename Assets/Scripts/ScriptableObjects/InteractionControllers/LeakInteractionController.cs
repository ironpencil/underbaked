using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeakInteractionController", menuName = "InteractionControllers/Leak")]
public class LeakInteractionController : InteractionController
{
    public List<Interaction> repairInteractions;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        if (repairInteractions.Contains(interaction))
        {
            Debug.Log("Fixin da leak!");
            Leak leak = target.GetComponent<Leak>();
            leak.FixLeak();
        }
    }
}
