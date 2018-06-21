using UnityEngine;

public class InteractableBehavior : MonoBehaviour, Interactable {
    public InteractionController interactionController;
    private InteractionController _interactionController;
    void Start() {
        Subscribe();
    }
    public virtual void OnInteract(GameObject interactor, Interaction interaction)
    {
        return;
    }
    public void Interact(GameObject interactor, Interaction interaction)
    {
        _interactionController.Interact(interactor, interaction);
    }
    public void Subscribe() {
        _interactionController = ScriptableObject.Instantiate(interactionController);
        _interactionController.Subscribe((Interactable)this);
    }
}