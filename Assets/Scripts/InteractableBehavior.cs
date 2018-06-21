using UnityEngine;

public class InteractableBehavior : MonoBehaviour, Interactable {
    public InteractionController ic;

    public virtual void Start() {
        ic.Subscribe((Interactable)this);
    }
    public virtual void OnInteract(GameObject interactor, Interaction interaction)
    {
        return;
    }
    public void Interact(GameObject interactor, Interaction interaction)
    {
        ic.Interact(interactor, interaction);
    }
}