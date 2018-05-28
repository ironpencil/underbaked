using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RefinerInteractionController", menuName = "InteractionControllers/Refiner")]
public class RefinerInteractionController : InteractionController
{
    public List<Interaction> refineInteractions;
    public List<FuelType> expectedTypes;
    public float startTime;
    public float completeTime;

    public override void Interact(GameObject target, GameObject interactor, Interaction interaction)
    {
        Carrier carrier = interactor.GetComponent<Carrier>();
        Workbench workbench = target.GetComponent<Workbench>();

        if (refineInteractions.Contains(interaction))
        {
            carrier.StartCoroutine(Refine(workbench, carrier));
        }
    }

    IEnumerator Refine(Workbench workbench, Carrier carrier) {
        Character character = carrier.GetComponent<Character>();
        
        if (character != null
        && carrier != null
        && carrier.heldObject != null) {
            
            Fuel fuel = carrier.heldObject.GetComponent<Fuel>();

            if (fuel != null && expectedTypes.Contains(fuel.fuelType)) {
                SetCharacterBusy(character);
                
                startTime = Time.time;
                completeTime = Time.time + workbench.refineTime;
                
                workbench.workSprite.enabled = true;
                SpriteRenderer startSprite = fuel.GetComponent<SpriteRenderer>();
                Color startColor = new Color(startSprite.color.r, startSprite.color.g, startSprite.color.b);
                Vector2 startScale = new Vector2(startSprite.transform.localScale.x, startSprite.transform.localScale.y);
                
                SpriteRenderer finishedSprite = workbench.productPrefab.GetComponent<SpriteRenderer>();

                carrier.Drop();
                Destroy(fuel.gameObject);

                while (Time.time < completeTime
                && character.movementState != Character.MovementState.MOVING) {
                    float timeFrac = (Time.time - startTime) / (completeTime - startTime);

                    workbench.workSprite.color = GetLerpColor(startColor, finishedSprite.color, timeFrac);
                    workbench.workSprite.transform.localScale = GetLerpScale(startScale, finishedSprite.transform.localScale, timeFrac);

                    yield return new WaitForSeconds(0.1f);
                }

                if (Time.time >= completeTime) {
                    workbench.workSprite.enabled = false;
                    carrier.PickUp(Instantiate(workbench.productPrefab, workbench.transform));
                }
            }

            SetCharacterIdle(character);
        }
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
