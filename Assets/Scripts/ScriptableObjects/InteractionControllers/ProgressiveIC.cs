using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Progressive")]
public class ProgressiveIC : InteractionController {
    public bool resetProgress = true;
    public Progression progression;
    public float completionTime = -1f;
    public bool failed = false;
    protected Character character;
    public bool completeAutomatically = false;
    
    public override void Interact(GameObject interactor, Interaction interaction) {
        Debug.Log("Interactor: " + interactor.name);
        Debug.Log("Interaction: " + interaction.name);
        if (acceptedInteractions.Contains(interaction)) {
            Debug.Log("Accepted");
            foreach (Interactable interactable in subscribers) {
                Debug.Log("Interactable: " + interactable.GetType());
                interactable.OnInteract(interactor, interaction);
            }
            interactor.GetComponent<MonoBehaviour>().StartCoroutine(Progress(interactor, interaction));
        }
    }
    private IEnumerator Progress(GameObject interactor, Interaction interaction) {
        Debug.Log("started progress");
        character = interactor.GetComponent<Character>();

        // If we're holding an object, and there isn't already a job start a new job
        if (progression == null || resetProgress) {
            OnBegin(interactor, interaction);
            if (failed) yield break;
            progression = CreateJob();
        }

        // If the actor is a character, set them to busy
        if (character != null) {
            SetCharacterBusy(character);
        }

        OnStart(interactor, interaction);
        if (failed) yield break;

        while (progression.length == -1f || progression.elapsedTime < progression.length) {
            if (!completeAutomatically && character != null && character.movementState == Character.MovementState.MOVING) {
                OnStop(interactor, interaction);
                yield break;
            }
            progression.elapsedTime += Time.deltaTime;
            OnUpdate(interactor, interaction);
            yield return 0;
        }
        OnFinish(interactor, interaction);
        progression = null;
    }

    private Progression CreateJob() {
        Progression progression = new Progression();        
        progression.elapsedTime = 0;
        progression.length = completionTime;

        return progression;
    }
    public float GetPercentComplete() {
        return progression.elapsedTime / completionTime;
    }
    public void SetCharacterBusy(Character character) {
        character.movementState = Character.MovementState.BUSY;
    }

    public virtual void OnBegin(GameObject interactor, Interaction interaction) {
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnBegin(interactor, interaction);
            }
        }
    }

    public virtual void OnStart(GameObject interactor, Interaction interaction) {
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnStart(interactor, interaction, progression);
            }
        }
    }

    public virtual void OnUpdate(GameObject interactor, Interaction interaction) {
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnUpdate(interactor, interaction, progression);
            }
        }
    }

    public virtual void OnStop(GameObject interactor, Interaction interaction) {
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnStop(interactor, interaction, progression);
            }
        }
    }

    public virtual void OnFinish(GameObject interactor, Interaction interaction) {
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnFinish(interactor, interaction);
            }
        }
    }
}
