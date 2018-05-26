using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
	Room roomA;
	Room roomB;
	public DoorState state;
	public enum DoorState {
		OPENING, OPEN, CLOSING, CLOSED, BROKEN
	}
	public Room GetConnectedRoom(Room caller) {
		if (caller == roomA)  {
			return roomB;
		} else {
			return roomA;
		}
	}

	public void Open() {
		if (this.state == DoorState.CLOSED) {
			this.state = DoorState.OPENING;
		}
	}

	public void Close() {
		if (this.state == DoorState.OPEN) {
			this.state = DoorState.CLOSING;
		}
	}

	public void Break() {
		if (this.state != DoorState.BROKEN) {
			this.state = DoorState.BROKEN;
		}
	}

	public void Fix() {
		if (this.state == DoorState.BROKEN) {
			this.state = DoorState.OPEN;
		}
	}

    public void Interact(Character actor) {
		if (this.state == DoorState.CLOSED) {
			Open();
		} else {
			Close();
		}
    }
}
