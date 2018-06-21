using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ProgressiveInteractableBehavior {
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
			doorCollider.enabled = false;
			GetComponentInChildren<SpriteRenderer>().color = openColor;
		}
	}

	public void Close() {
		if (this.state == DoorState.OPEN) {
			this.state = DoorState.CLOSED;
			doorCollider.enabled = true;
            GetComponentInChildren<SpriteRenderer>().color = closedColor;
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

    public override void OnFinish(GameObject interactor, Interaction interaction)
    {
        if (state == DoorState.CLOSED)
        {
            Open();
        }
        else if (state == DoorState.OPEN)
        {
            Close();
        }
    }

	public bool IsOpen() {
		return this.state != DoorState.CLOSED;
	}
}
