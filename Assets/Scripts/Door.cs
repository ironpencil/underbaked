using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
	public DoorState state;
	public enum DoorState {
		OPENING, OPEN, CLOSING, CLOSED, BROKEN
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
