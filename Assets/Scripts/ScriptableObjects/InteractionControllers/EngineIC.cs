using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EngineIC", menuName = "InteractionControllers/Engine")]
public class EngineIC : ConsumerIC
{
    public override void OnConsume(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        Debug.Log("Yum! Good fuel!");
    }
}