using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour {
	public Carryable heldObject;
	public Transform carryPosition;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PickUp(Carryable carryable) {
		heldObject = carryable;
		carryable.transform.SetParent(carryPosition);
		carryable.transform.localPosition = new Vector2(0f, 0f);
	}

	public void Drop() {
		heldObject.transform.SetParent(transform.parent);
		heldObject.transform.position = carryPosition.position;
		heldObject = null;
	}
}
