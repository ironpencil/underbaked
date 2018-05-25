using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Class for handling player input
 */
public class PlayerInput : MonoBehaviour {

	private Rigidbody2D rb;
	public float defaultSpeed;
	private float currentSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();

	}
	
	void OnUpdate() {
		
	}

	void OnTriggerStay2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (IsActionButtonPressed() && IsObjectInteractable(otherObject)) {
			InteractWith(otherObject);
		}
	}

	void InteractWith(GameObject otherObject) {
		otherObject.GetComponent<Interactable>().Interact();
	}

	bool IsObjectInteractable(GameObject gameObject) {
		Component c = gameObject.GetComponent<Interactable>();
		return c != null;
	}

	void FixedUpdate () {
		HandleMovement();
	}

	void HandleMovement() {
		Vector2 moveForce = GetMovementDirection() * GetCurrentPlayerSpeed();

		rb.AddForce(moveForce, ForceMode2D.Force);
	}

	private bool IsActionButtonPressed() {
		return Input.GetButtonDown("Fire1");
	}

	private Vector2 GetMovementDirection() {
		return new Vector2(GetPlayerHorizontal(), GetPlayerVertical());
	}

	private float GetCurrentPlayerSpeed() {
		return defaultSpeed;
	}

	private float GetPlayerHorizontal() {
		float horizontal = Input.GetAxis("Horizontal");
		return horizontal;
	}

	private float GetPlayerVertical() {
		float vertical = Input.GetAxis("Vertical");
		return vertical;
	}
}
