using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Interaction Controller")]
public class InteractionController : ScriptableObject {
    public List<Interaction> acceptedInteractions;
    public List<Interactable> subscribers = new List<Interactable>();

    public void Subscribe(Interactable interactable) {
        subscribers.Add(interactable);
    }

    public void Unsubscribe(Interactable interactable) {
        subscribers.Remove(interactable);
    }

    public virtual void Interact(GameObject interactor, Interaction interaction) {
        //Debug.Log("Interactor: " + interactor.name);
        //Debug.Log("Interaction: " + interaction.name);
        if (acceptedInteractions.Contains(interaction)) {
            //Debug.Log("Accepted");
            foreach (Interactable interactable in subscribers) {
                //Debug.Log("Interactable: " + interactable.GetType());
                interactable.OnInteract(interactor, interaction);
            }
        }
    }
}
