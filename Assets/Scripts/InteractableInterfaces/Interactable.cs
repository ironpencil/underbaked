using UnityEngine;

public interface Interactable
{
    void OnInteract(GameObject interactor, Interaction interaction);
}