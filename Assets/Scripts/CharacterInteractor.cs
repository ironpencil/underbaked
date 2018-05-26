using System.Collections;
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

    }

    public void ClearInteractables() {
        interactables.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        if (IsObjectInteractable(otherObject))
        {
            Debug.Log("Entered interactable");
            interactables.Add(otherObject.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        if (IsObjectInteractable(otherObject))
        {
            Debug.Log("Exited interactable");
            interactables.Remove(otherObject.GetComponent<Interactable>());
        }
    }

    bool IsObjectInteractable(GameObject gameObject)
    {
        Component c = gameObject.GetComponent<Interactable>();
        return c != null;
    }

    public void Interact()
    {
        if (character.isAlive)
        {
            character.movementState = Character.MovementState.BUSY;
            if (character.heldObject != null)
            {
                character.Drop();
            }
            else
            {
                foreach (Interactable interactable in interactables)
                {
                    interactable.Interact(gameObject, interaction);
                }
            }
        }
    }
}
