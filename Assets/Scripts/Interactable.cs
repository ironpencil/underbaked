using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
	public Collider2D triggerZone;

	public abstract void Interact();
}
