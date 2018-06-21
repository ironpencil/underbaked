using UnityEngine;

public class ProgressiveInteractableBehavior : InteractableBehavior, Progressive {
    public virtual void OnBegin(GameObject interactor, Interaction interaction)
    {
        return;
    }

    public virtual void OnStart(GameObject interactor, Interaction interaction, Progression progression)
    {
        return;
    }

    public virtual void OnUpdate(GameObject interactor, Interaction interaction, Progression progression)
    {
        return;
    }

    public virtual void OnStop(GameObject interactor, Interaction interaction, Progression progression)
    {
        return;
    }

    public virtual void OnFinish(GameObject interactor, Interaction interaction)
    {
        return;
    }
}