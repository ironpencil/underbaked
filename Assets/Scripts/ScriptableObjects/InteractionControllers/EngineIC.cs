using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EngineIC", menuName = "InteractionControllers/Engine")]
public class EngineIC : ProgressiveConsumerIC
{
    public override void OnBegin(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        
    }

    public override void OnFinish(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        Debug.Log("Yum! Good fuel!");
    }

    public override void OnStart(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        
    }

    public override void OnStop(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        
    }

    public override void OnUpdate(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        
    }
}