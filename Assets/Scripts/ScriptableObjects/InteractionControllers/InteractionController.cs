using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Interaction Controller")]
public class InteractionController : ScriptableObject {
    public List<Interaction> acceptedInteractions;
    protected List<Interactable> subscribers;

    public void Subscribe(Interactable interactable) {
        subscribers.Add(interactable);
    }

    public void Unsubscribe(Interactable interactable) {
        subscribers.Remove(interactable);
    }

    public virtual void Interact(GameObject interactor, Interaction interaction) {
        if (acceptedInteractions.Contains(interaction)) {
            foreach (Interactable interactable in subscribers) {
                interactable.OnInteract(interactor, interaction);
            }
        }
    }
}
