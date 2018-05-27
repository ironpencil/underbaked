using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorInteractionController", menuName = "InteractionControllers/Door")]
public class DoorInteractionController : InteractionController
{

    public List<Interaction> operateInteractions;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        Door door = target.GetComponent<Door>();

        if (operateInteractions.Contains(interaction))
        {
            if (door.state == Door.DoorState.CLOSED)
            {
                door.Open();
            }
            else if (door.state == Door.DoorState.OPEN)
            {
                door.Close();
            }
        }
    }
}
