using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorIC", menuName = "InteractionControllers/Door")]
public class DoorIC : ProgressiveIC
{
    public override void OnBegin(GameObject interactor, GameObject target, Interaction interaction)
    {
        
    }

    public override void OnFinish(GameObject interactor, GameObject target, Interaction interaction)
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

    public override void OnStart(GameObject interactor, GameObject target, Interaction interaction)
    {
        
    }

    public override void OnStop(GameObject interactor, GameObject target, Interaction interaction)
    {
        
    }

    public override void OnUpdate(GameObject interactor, GameObject target, Interaction interaction)
    {
        
    }
}
