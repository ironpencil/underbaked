using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public float defaultSpeed;
	private float currentSpeed;
	private Rigidbody2D rb;
	private List<Status> statuses;
	public Respawner respawner;
	public bool isAlive;
	public enum MovementState {
		MOVING, IDLE, BUSY, DEAD
	}
	public MovementState movementState = MovementState.IDLE;
    public Animator animator;

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
			if (movementState != MovementState.BUSY) {
				if (Vector2.zero == moveForce) {
					movementState = MovementState.IDLE;
					animator.SetBool("isWalking", false);
				} else {
					movementState = MovementState.MOVING;
					animator.SetBool("isWalking", true);
					Vector3 scale = transform.localScale;
					if (direction.x < 0) { scale.x = 1; } else if (direction.x > 0) { scale.x = -1; }
					transform.localScale = scale;
					rb.AddForce(moveForce, ForceMode2D.Force);
				}
			}
		}
	}

	public void Die() {
		Carrier carrier = GetComponent<Carrier>();
		if (carrier != null) {
			carrier.Drop();
		}
		respawner.StartRespawn(this);
		isAlive = false;
		movementState = MovementState.DEAD;
		gameObject.SetActive(false);
	}

	public void Revive(Vector2 location) {
		isAlive = true;
		GetComponent<CharacterInteractor>().ClearInteractables();
		gameObject.transform.position = location;
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
