using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeder : Carrier {
	public Consumable GetConsumable() {
		Consumable consumable = null;

		if (heldObject != null) {
			consumable = heldObject.GetComponent<Consumable>();
		}

		return consumable;
	}
	public void Feed() {
		GameObject consumable = heldObject.gameObject;
        Drop();
        Destroy(consumable);
	}
}
