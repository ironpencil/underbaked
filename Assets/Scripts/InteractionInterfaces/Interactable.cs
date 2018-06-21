using UnityEngine;

public interface Interactable
{
    void OnInteract(GameObject interactor, Interaction interaction);

    void Interact(GameObject interactor, Interaction interaction);
}