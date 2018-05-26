using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Interactable : MonoBehaviour {
	public InteractionController interactionController;
    public GameObject target;

	public void Interact(GameObject interactor, Interaction interaction) {
        Assert.IsNotNull(interactionController, $"Interactable on {gameObject.name} has a null interaction.");

        if (target == null) { target = gameObject; }
        interactionController.Interact(target, interactor, interaction);
	}
}
