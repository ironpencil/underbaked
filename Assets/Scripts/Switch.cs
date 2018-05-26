using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    bool isOn = true;
	public Color onColor;
	public Color offColor;

	public void Toggle() {
		isOn = !isOn;
		if (isOn) {
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
