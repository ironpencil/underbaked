using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwitchInteractionController", menuName = "InteractionControllers/Switch")]
public class SwitchInteractionController : InteractionController
{
    public List<Interaction> fixInteractions;
    public List<Interaction> operateInteractions;
    public List<Interaction> breakInteractions;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        Switch theSwitch = target.GetComponent<Switch>();

        if (breakInteractions.Contains(interaction))
        {
            Character character = interactor.GetComponent<Character>();
            if (character != null)
            {
                character.Die();
            }
        }
        
        if (operateInteractions.Contains(interaction))
        {
            theSwitch.Toggle();
        }

        
    }
}
