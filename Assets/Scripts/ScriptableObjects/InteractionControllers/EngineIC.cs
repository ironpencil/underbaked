using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EngineIC", menuName = "InteractionControllers/Engine")]
public class EngineIC : ProgressiveConsumerIC
{
    public EngineStats statsPrefab;
    private EngineStats stats;

    public void OnEnable() {
        stats = ScriptableObject.Instantiate(statsPrefab);
    }

    public override void OnBegin(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        
    }

    public override void OnFinish(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        stats.AddEnergy(consumable.consumableType.energyValue);
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

    public void BurnEnergy(float deltaTime) {
        stats.BurnEnergy(deltaTime);
    }

    public float GetStepFreq() {
        return stats.GetStepFreq();
    }
}