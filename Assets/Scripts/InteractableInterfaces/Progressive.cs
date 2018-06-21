using UnityEngine;

public interface Progressive
{
    void OnBegin(GameObject interactor, Interaction interaction);
    void OnStart(GameObject interactor, Interaction interaction, Progression progression);
    void OnUpdate(GameObject interactor, Interaction interaction, Progression progression);
    void OnStop(GameObject interactor, Interaction interaction, Progression progression);
    void OnFinish(GameObject interactor, Interaction interaction);
}