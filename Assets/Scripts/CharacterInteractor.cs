using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : Interactor
{
    public Interaction interaction;
    private List<InteractableBehavior> interactables;
    private Character character;

    // Use this for initialization
    void Start()
    {
        interactables = new List<InteractableBehavior>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = interactables.Count - 1; i >= 0; i--) {
            InteractableBehavior interactable = interactables[i];
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
            //Debug.Log("Entered interactable: " + otherObject.transform.name);
            InteractableBehavior interactable = otherObject.GetComponent<InteractableBehavior>();
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
            interactables.Remove(otherObject.GetComponent<InteractableBehavior>());
        }
    }

    bool IsObjectInteractable(GameObject gameObject)
    {
        Component c = gameObject.GetComponent<InteractableBehavior>();
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
            bool interacted = false;

            // Using index iteration because the interactable might be destroyed 
            // through the interaction. Iterating in reverse in case an element 
            // is removed.
            for (int i = interactables.Count - 1; i >= 0; i--) {
                InteractableBehavior interactable = interactables[i];
                interactable.Interact(gameObject, interaction);
                interacted = true;
            }

            // There is no way to know that an interaction really occurred, so
            // we just need to test if there are any interactables around.
            if (!interacted) {
                Carrier carrier = character.GetComponent<Carrier>();
                if (carrier != null && carrier.heldObject != null)
                {
                    carrier.Drop();
                }
            }
            character.movementState = Character.MovementState.IDLE;
        }
    }
}
