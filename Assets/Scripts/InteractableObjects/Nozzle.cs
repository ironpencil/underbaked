using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nozzle : Carryable {
	public FloodBehavior flood;
	private float elapsed;
	private float length;
	private float amount;

	// Update is called once per frame
	void Update () {
		if (IsPumping() && flood != null) {
			elapsed += Time.deltaTime;
			flood.ChangeWaterValue(-(Time.deltaTime / length) * amount);
		}
	}

	public void StartPumping(float length, float amount) {
		this.elapsed = 0;
		this.length = length;
		this.amount = amount;
	}

	public bool IsPumping() {
		return elapsed < length;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		FloodBehavior otherFlood = other.GetComponentInChildren<FloodBehavior>();
		
		if (otherFlood != null) {
			flood = otherFlood;
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		FloodBehavior otherFlood = other.GetComponent<FloodBehavior>();
		
		if (otherFlood == flood) {
			flood = null;
		}
	}
}
