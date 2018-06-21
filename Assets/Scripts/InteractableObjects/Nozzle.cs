using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nozzle : Carryable {
	public Room room;
	private float elapsed;
	private float length;
	private float amount;

	// Update is called once per frame
	void Update () {
		if (IsPumping()) {
			elapsed += Time.deltaTime;
			room.ChangeWaterValue(-(Time.deltaTime / length) * amount);
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

	// Did not implement OnTriggerExit2D because a nozzle
	// should always be in some room
	public void OnTriggerEnter2D(Collider2D other) {
		Room otherRoom = other.GetComponent<Room>();
		
		if (otherRoom != null) {
			room = otherRoom;
		}
	}
}
