using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour {
	public Carryable heldObject;
	public Transform carryPosition;

	public void PickUp(Carryable carryable) {
		Debug.Log("Picking up...");
		heldObject = carryable;
		carryable.transform.SetParent(carryPosition);
		carryable.transform.localPosition = new Vector2(0f, 0f);
		carryable.gameObject.layer = LayerMask.NameToLayer("Carried");
		Rigidbody2D rb = carryable.GetComponent<Rigidbody2D>();
		if (rb != null) {
			rb.isKinematic = true;
		}
	}

	public void Drop() {
		Rigidbody2D rb = heldObject.GetComponent<Rigidbody2D>();
		if (rb != null) {
			rb.isKinematic = false;
		}
		heldObject.gameObject.layer = LayerMask.NameToLayer("Default");
		heldObject.transform.SetParent(carryPosition);
		heldObject.transform.position = carryPosition.position;
		heldObject = null;
	}
}
