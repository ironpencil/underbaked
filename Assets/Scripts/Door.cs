using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public DoorState state;
	public enum DoorState {
		OPEN, OPENING, CLOSING, CLOSED, BROKEN
	}

	public void Open() {
		if (this.state == DoorState.CLOSED) {
			this.state = DoorState.OPEN;
            Debug.Log("Opened door!");
		}
	}

	public void Close() {
		if (this.state == DoorState.OPEN) {
			this.state = DoorState.CLOSED;
            Debug.Log("Closed door!");
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
}
