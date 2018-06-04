using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorIC", menuName = "InteractionControllers/Door")]
public class DoorInteractionController : InteractionController
{
    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction)
    {
        Door door = target.GetComponent<Door>();

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
