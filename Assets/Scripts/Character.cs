using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public float defaultSpeed;
	private float currentSpeed;
	private Rigidbody2D rb;
	private List<Status> statuses;
	public Carryable heldObject;
	public bool isAlive;
	public enum MovementState {
		MOVING, IDLE, BUSY, DEAD
	}
	public MovementState movementState = MovementState.IDLE;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		statuses = new List<Status>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAlive) {
			foreach(Status status in statuses) {
				status.Update(Time.deltaTime);
			}
		}
	}

	public void Move(Vector2 direction) {
		if (isAlive) {
			Vector2 moveForce = direction * GetCurrentPlayerSpeed();
			if (Vector2.zero == moveForce) {
				movementState = MovementState.IDLE;
			} else {
				movementState = MovementState.MOVING;
				rb.AddForce(moveForce, ForceMode2D.Force);
			}
		}
	}

	public void Drop() {
		heldObject = null;
	}

	public void Die() {
		Drop();
		isAlive = false;
		movementState = MovementState.DEAD;
		gameObject.SetActive(false);
	}

	public void Revive() {
		isAlive = true;
		gameObject.SetActive(true);
	}

	private float GetCurrentPlayerSpeed() {
		if (isAlive) {
			return defaultSpeed;
		} else {
			return 0;
		}
	}
}
