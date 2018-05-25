using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : Interactable {
    bool state = true;
	public Color onColor;
	public Color offColor;

	public override void Interact()
    {
        ToggleState();
    }

	public void ToggleState() {
		state = !state;
		if (state) {
			gameObject.GetComponent<SpriteRenderer>().color = onColor;
		} else {
			gameObject.GetComponent<SpriteRenderer>().color = offColor;
		}
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
