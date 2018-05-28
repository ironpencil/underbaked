﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : Interactor
{
    public Interaction interaction;
    private List<Interactable> interactables;
    private Character character;

    // Use this for initialization
    void Start()
    {
        interactables = new List<Interactable>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = interactables.Count - 1; i >= 0; i--) {
            Interactable interactable = interactables[i];
            if (!interactable) {
                interactables.RemoveAt(i);
            }
        }
    }

    public void ClearInteractables() {
        interactables.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        if (IsObjectInteractable(otherObject))
        {
            Debug.Log("Entered interactable: " + otherObject.transform.name);
            Interactable interactable = otherObject.GetComponent<Interactable>();
            if (!interactables.Contains(interactable)) {
                interactables.Add(interactable);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        if (IsObjectInteractable(otherObject))
        {
            Debug.Log("Exited interactable: " + otherObject.transform.name);
            interactables.Remove(otherObject.GetComponent<Interactable>());
        }
    }

    bool IsObjectInteractable(GameObject gameObject)
    {
        Component c = gameObject.GetComponent<Interactable>();
        return c != null;
    }

    /**
     * Note: This current implentation creates goofy situations with carryables.
     *       If the character is standing near an interactable, and a carryable
     *       if the carryable interaction executes first, the carryable will get
     *       picked up, and the other object will not be interacted with. If the
     *       interactable interaction executes first, that interaction will occur
     *       AND the carryable will be picked up.
     */
    public void Interact()
    {
        if (character.isAlive)
        {
            character.movementState = Character.MovementState.BUSY;
            Carrier carrier = character.GetComponent<Carrier>();
            if (carrier != null && carrier.heldObject != null)
            {
                carrier.Drop();
            }
            else
            {
                // Using index iteration because the interactable might be destroyed 
                // through the interaction. Iterating in reverse in case an element 
                // is removed.
                for (int i = interactables.Count - 1; i >= 0; i--) {
                    Interactable interactable = interactables[i];
                    interactable.Interact(gameObject, interaction);
                }
            }
        }

        Debug.Log("Done interacting");
    }
}
