using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorInteraction", menuName = "Interactions/Door")]
public class DoorInteraction : Interaction
{
    public override void Interact(GameObject target)
    {
        Door theDoor = target.GetComponent<Door>();

        if (theDoor.state == Door.DoorState.CLOSED)
        {
            theDoor.Open();
        }
        else if (theDoor.state == Door.DoorState.OPEN)
        {
            theDoor.Close();
        }
    }
}
