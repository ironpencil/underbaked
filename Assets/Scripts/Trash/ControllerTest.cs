using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 3.0f;
    public float bulletSpeed = 15.0f;

    private Player player; // The Rewired Player
    private Vector3 moveVector;
    private bool action;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("moveHorizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("moveVertical");
        action = player.GetButtonDown("action");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            Vector3 newPos = transform.localPosition;
            newPos += (moveVector * moveSpeed * Time.deltaTime);
            transform.localPosition = newPos;
        }

        // Process fire
        if (action)
        {
            Debug.Log("Action Pressed!");
        }
    }
}
