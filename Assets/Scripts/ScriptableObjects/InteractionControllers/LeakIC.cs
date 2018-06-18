using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeakIC", menuName = "InteractionControllers/Leak")]
public class LeakIC : ProgressiveIC
{
    private Leak leak;

    public override void OnBegin(GameObject interactor, GameObject target, Interaction interaction)
    {
        Debug.Log("Leak on begin");
        Debug.Log("Getting leak component");
        leak = target.GetComponent<Leak>();
    }

    public override void OnFinish(GameObject interactor, GameObject target, Interaction interaction)
    {
        Debug.Log("Marking leak as fixed");
        leak.FixLeak();
    }

    public override void OnStart(GameObject interactor, GameObject target, Interaction interaction)
    {
       Debug.Log("Leak on start");
    }

    public override void OnStop(GameObject interactor, GameObject target, Interaction interaction)
    {
        Debug.Log("Leak on stop");
    }

    public override void OnUpdate(GameObject interactor, GameObject target, Interaction interaction)
    {
        Debug.Log("Leak on update");
    }
}
