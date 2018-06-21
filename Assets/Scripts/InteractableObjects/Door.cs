using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ProgressiveInteractableImpl {
	public DoorState state;
	private BoxCollider2D doorCollider;
	public Color openColor;
	public Color closedColor;
	public Color closingColor;

	public enum DoorState {
		OPEN, OPENING, CLOSING, CLOSED, BROKEN
	}
	void Start() {
		Subscribe();
		foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>()) {
			if (!collider.isTrigger) {
				doorCollider = collider;
			}
		}
	}

    public override void OnFinish(GameObject interactor, Interaction interaction)
    {
        if (state == DoorState.OPENING)
        {
            Open();
        }
        else if (state == DoorState.CLOSING)
        {
            Close();
        }
    }
	public override void OnBegin(GameObject interactor, Interaction interaction)
    {
		if (this.state == DoorState.OPEN) {
			this.state = DoorState.CLOSING;
		} else if (this.state == DoorState.CLOSED) {
			this.state = DoorState.OPENING;
			GetComponentInChildren<SpriteRenderer>().color = closingColor;
		}
		
    }
	public void Open() {
		this.state = DoorState.OPEN;
		doorCollider.enabled = false;
		GetComponentInChildren<SpriteRenderer>().color = openColor;
	}
	public void Close() {
		this.state = DoorState.CLOSED;
		doorCollider.enabled = true;
		GetComponentInChildren<SpriteRenderer>().color = closedColor;
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
