using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RefinerInteractionController", menuName = "InteractionControllers/Refiner")]
public class RefinerInteractionController : InteractionController
{
    public List<Interaction> refineInteractions;
    public List<FuelType> expectedTypes;
    private RefineJob job;
    private const float WAIT_LENGTH = 0.1f;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        if (refineInteractions.Contains(interaction))
        {
            target.GetComponent<MonoBehaviour>().StartCoroutine(Refine(target, interactor));
        }
    }

    IEnumerator Refine(GameObject target, GameObject interactor) {
        Workbench workbench = target.GetComponent<Workbench>();
        Carrier carrier = interactor.GetComponent<Carrier>();
        Character character = carrier.GetComponent<Character>();
        
        if (character != null && carrier != null) {
            Fuel fuel = null;
            if (carrier.heldObject != null) {
                fuel = carrier.heldObject.GetComponent<Fuel>();
            }

            if (fuel != null 
            && job == null 
            && expectedTypes.Contains(fuel.fuelType)) {
                SpriteRenderer startSprite = fuel.GetComponent<SpriteRenderer>();
                SpriteRenderer finishedSprite = workbench.productPrefab.GetComponent<SpriteRenderer>();

                job = CreateJob(startSprite, finishedSprite, workbench.refineTime);

                carrier.Drop();
                Destroy(fuel.gameObject);
                workbench.workSprite.enabled = true;
            }
            
            if (job != null && carrier.heldObject == null) {
                SetCharacterBusy(character);
                job.startWorking = Time.time;

                while (job.elapsedTime < job.length
                && character.movementState != Character.MovementState.MOVING) {
                    job.elapsedTime += WAIT_LENGTH + Time.deltaTime;
                    float timeFrac = job.elapsedTime / job.length;

                    workbench.workSprite.color = GetLerpColor(job.startColor, job.finishColor, timeFrac);
                    workbench.workSprite.transform.localScale = GetLerpScale(job.startScale, job.finishScale, timeFrac);

                    yield return new WaitForSeconds(WAIT_LENGTH);
                }

                if (job.elapsedTime > job.length) {
                    job = null;
                    workbench.workSprite.enabled = false;
                    carrier.PickUp(Instantiate(workbench.productPrefab, workbench.transform));
                }
            }

            SetCharacterIdle(character);
        }
    }

    private RefineJob CreateJob(SpriteRenderer startSprite, SpriteRenderer finishedSprite, float length) {
        RefineJob newJob = new RefineJob();        
        newJob.elapsedTime = 0;
        newJob.length = length;
        newJob.startColor = new Color(startSprite.color.r, startSprite.color.g, startSprite.color.b);
        newJob.startScale = new Vector2(startSprite.transform.localScale.x, startSprite.transform.localScale.y);
        newJob.finishColor = finishedSprite.color;
        newJob.finishScale = finishedSprite.transform.localScale;

        return newJob;
    }

    private Vector2 GetLerpScale(Vector2 startScale, Vector2 finishScale, float percentComplete)
    {
        float x = Mathf.Lerp(startScale.x, finishScale.x, percentComplete);
        float y = Mathf.Lerp(startScale.y, finishScale.y, percentComplete);
        return new Vector2(x, y);
    }

    Color GetLerpColor(Color startColor, Color finishColor, float percentComplete) {
        float red = Mathf.Lerp(startColor.r, finishColor.r, percentComplete);
        float green = Mathf.Lerp(startColor.g, finishColor.g, percentComplete);
        float blue = Mathf.Lerp(startColor.b, finishColor.b, percentComplete);
        return new Color(red, green, blue);
    }
}
