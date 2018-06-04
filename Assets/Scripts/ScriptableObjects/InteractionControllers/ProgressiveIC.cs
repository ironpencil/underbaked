using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgressiveIC : InteractionController {
    public bool resetProgress;
    public Progression progression;
    public float completionTime;
    public bool failed = false;
    protected MonoBehaviour mono;
    protected Character character;

    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction) {
        failed = false;
        target.GetComponent<MonoBehaviour>().StartCoroutine(Progress(interactor, target, interaction));
    }

    private IEnumerator Progress(GameObject interactor, GameObject target, Interaction interaction) {
        mono = interactor.GetComponent<MonoBehaviour>();
        character = interactor.GetComponent<Character>();

        // If we're holding an object, and there isn't already a job start a new job
        if (progression == null || resetProgress) {
            OnBegin(interactor, target, interaction);
            if (failed) yield break;
            progression = CreateJob();
        }

        // If the actor is a character, set them to busy
        if (character != null) {
            SetCharacterBusy(character);
        }

        OnStart(interactor, target, interaction);
        if (failed) yield break;

        while (progression.elapsedTime < progression.length) {
            if (character != null && character.movementState == Character.MovementState.MOVING) {
                Debug.Log("Moved!");
                OnStop(interactor, target, interaction);
                yield break;
            }
            progression.elapsedTime += Time.deltaTime;
            OnUpdate(interactor, target, interaction);
            yield return 0;
        }
        OnFinish(interactor, target, interaction);
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

    public abstract void OnBegin(GameObject interactor, GameObject target, Interaction interaction);

    public abstract void OnStart(GameObject interactor, GameObject target, Interaction interaction);

    public abstract void OnUpdate(GameObject interactor, GameObject target, Interaction interaction);

    public abstract void OnStop(GameObject interactor, GameObject target, Interaction interaction);

    public abstract void OnFinish(GameObject interactor, GameObject target, Interaction interaction);
}
