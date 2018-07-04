using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

/**
 * 	Class for handling player input
 */
public class PlayerCharacterInput : MonoBehaviour
{
    private Player player;
    private Character character;
    private CharacterInteractor characterInteractor;
    public int playerId = 0;
    public GameEvent pauseEvent;
    public GameState gameState;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Start()
    {
        character = GetComponent<Character>();
        characterInteractor = GetComponent<CharacterInteractor>();
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
        if (!gameState.isPaused && gameState.isInMission) {
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
