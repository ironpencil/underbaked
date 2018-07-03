using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

/**
 * 	Class for handling player input
 */
public class PlayerInput : MonoBehaviour
{
    private Player player;
    public Character character;
    public CharacterInteractor characterInteractor;
    public int playerId = 0;
    public GameEvent pauseEvent;
    public GameState gameState;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (player.GetButtonDown("pause"))
        {
            pauseEvent.Raise();
        }
    }

    void FixedUpdate()
    {
        if (!gameState.isPaused) {
            HandleMovement();
            HandleActionPress();
        }
    }

    void HandleActionPress()
    {
        if (player.GetButtonDown("action"))
        {
            characterInteractor.Interact();
        }   
    }

    void HandleMovement()
    {
        character.Move(new Vector2(player.GetAxis("moveHorizontal"), player.GetAxis("moveVertical")));
    }
}
