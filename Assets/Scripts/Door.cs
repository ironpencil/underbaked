using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public DoorState state;
	// TODO May want to remove this, just used for testing doors
	public BoxCollider2D doorCollider;
	// TODO Remove these when done testing
	public Color openColor;
	public Color closedColor;

	public enum DoorState {
		OPEN, OPENING, CLOSING, CLOSED, BROKEN
	}

	public void Open() {
		if (this.state == DoorState.CLOSED) {
			this.state = DoorState.OPEN;
            Debug.Log("Opened door!");
			doorCollider.enabled = false;
			GetComponent<SpriteRenderer>().color = openColor;
		}
	}

	public void Close() {
		if (this.state == DoorState.OPEN) {
			this.state = DoorState.CLOSED;
            Debug.Log("Closed door!");
			doorCollider.enabled = true;
			GetComponent<SpriteRenderer>().color = closedColor;
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

	public bool IsOpen() {
		return this.state != DoorState.CLOSED;
	}
}
