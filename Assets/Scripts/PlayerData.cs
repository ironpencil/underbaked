using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	private PlayerInput playerInput;
	public Character character;

    // Use this for initialization
    void Start () {
		playerInput = GetComponent<PlayerInput>();
		playerInput.character = character;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
