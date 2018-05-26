using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Interactable : MonoBehaviour {
	public Interaction interaction;
    public GameObject target;

	public void Interact(Character actor) {
        Assert.IsNotNull(interaction, $"Interactable on {gameObject.name} has a null interaction.");

        if (target == null) { target = gameObject; }
        interaction.Interact(target);
	}
}
