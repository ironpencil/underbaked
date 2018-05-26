using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour, IInteractable {
    bool state = true;
	public Color onColor;
	public Color offColor;

	public void Interact(Character actor)
    {
        ToggleState();
    }

	public void ToggleState() {
		state = !state;
		if (state) {
			GetComponent<SpriteRenderer>().color = onColor;
		} else {
			GetComponent<SpriteRenderer>().color = offColor;
		}
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
