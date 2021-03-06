﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour {
	public Carryable heldObject;
	public Transform carryPosition;

	public void PickUp(Carryable carryable) {
		if (carryable != null) {
			if (heldObject != null) {
				Drop();
			}
			heldObject = carryable;
			heldObject.carrier = this;
			carryable.transform.SetParent(carryPosition);
			carryable.transform.localPosition = new Vector2(0f, 0f);
			carryable.gameObject.layer = LayerMask.NameToLayer("Carried");
			Rigidbody2D rb = carryable.GetComponent<Rigidbody2D>();
			if (rb != null) {
				rb.isKinematic = true;
			}
		}
	}

	public void Drop() {
		if (heldObject != null) {
			Rigidbody2D rb = heldObject.GetComponent<Rigidbody2D>();
			if (rb != null) {
				rb.isKinematic = false;
			}
			heldObject.carrier = null;
			heldObject.gameObject.layer = LayerMask.NameToLayer("Default");
			heldObject.transform.SetParent(transform.parent);
			heldObject.transform.position = carryPosition.position;
			heldObject = null;
		}
	}
}
