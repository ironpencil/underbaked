using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionController : ScriptableObject {

    public abstract void Interact(GameObject target, GameObject interactor, Interaction interaction);

    public void SetCharacterBusy(GameObject gameObject) {
        Character character = gameObject.GetComponent<Character>();
        if (character != null) {
            SetCharacterBusy(character);
        }
    }

    public void SetCharacterBusy(Character character) {
        character.movementState = Character.MovementState.BUSY;
    }

    public void SetCharacterIdle(GameObject gameObject) {
        Character character = gameObject.GetComponent<Character>();
        if (character != null) {
            SetCharacterIdle(character);
        }
    }

    public void SetCharacterIdle(Character character) {
        character.movementState = Character.MovementState.IDLE;
    }
}
