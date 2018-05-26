using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorInteractionController", menuName = "InteractionControllers/Door")]
public class DoorInteractionController : InteractionController
{

    public List<Interaction> operateInteractions;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        Door theDoor = target.GetComponent<Door>();

        if (operateInteractions.Contains(interaction))
        {
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
}
