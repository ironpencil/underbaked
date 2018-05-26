using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableComponent : MonoBehaviour {
	public MonoBehaviour target;
	public Collider2D triggerZone;

	void Start() {
		target = GetComponent<SwitchBehavior>();
	}

	public void Interact(Character actor) {
		if (target != null) {
			((IInteractable)target).Interact(actor);
		}
	}
}
