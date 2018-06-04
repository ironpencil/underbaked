using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeakIC", menuName = "InteractionControllers/Leak")]
public class LeakIC : ProgressiveIC
{
    private Leak leak;
    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction)
    {
        leak = target.GetComponent<Leak>();
    }

    public override void OnBegin(GameObject interactor, GameObject target, Interaction interaction)
    {
        
    }

    public override void OnFinish(GameObject interactor, GameObject target, Interaction interaction)
    {
        leak.FixLeak();
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
