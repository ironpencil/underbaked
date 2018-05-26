using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwitchInteraction", menuName = "Interactions/Switch")]
public class SwitchInteraction : Interaction
{
    public override void Interact(GameObject target)
    {
        Switch theSwitch = target.GetComponent<Switch>();

        theSwitch.Toggle();
    }
}
