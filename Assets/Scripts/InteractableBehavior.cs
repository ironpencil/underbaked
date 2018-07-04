using UnityEngine;

public class InteractableBehavior : MonoBehaviour, Interactable {
    public InteractionController interactionController;
    private InteractionController _interactionController;
    public virtual void Start() {
        Subscribe();
    }
    public virtual void OnInteract(GameObject interactor, Interaction interaction)
    {
        return;
    }
    public void Interact(GameObject interactor, Interaction interaction)
    {
        Debug.Log("Interactor: " + interactor.name);
        Debug.Log("Interaction: " + interaction.name);
        Debug.Log("Interactable: " + gameObject.name);
        _interactionController.Interact(interactor, interaction);
    }
    public void Subscribe() {
        _interactionController = ScriptableObject.Instantiate(interactionController);
        _interactionController.Subscribe((Interactable)this);
    }
}