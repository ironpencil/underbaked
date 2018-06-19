using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RefinerIC", menuName = "InteractionControllers/Refiner")]
public class RefinerIC : ProgressiveConsumerProducerIC {
    public Color startColor;
    public Color finishColor;
    public Vector2 startScale;
    public Vector2 finishScale;

    public override void OnBegin(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        startColor = consumable.GetComponent<SpriteRenderer>().color;
        finishColor = product.GetComponent<SpriteRenderer>().color;
        startScale = consumable.transform.localScale;
        finishScale = product.transform.localScale;
        workbench.workSprite.enabled = true;
    }

    public override void OnStart(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        
    }

    public override void OnUpdate(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        workbench.workSprite.color = GetLerpColor(startColor, finishColor, GetPercentComplete());
        workbench.workSprite.transform.localScale = GetLerpScale(startScale, finishScale, GetPercentComplete());
    }

    public override void OnStop(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        
    }

    public override void OnFinish(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        workbench.workSprite.enabled = false;
    }

    private Vector2 GetLerpScale(Vector2 startScale, Vector2 finishScale, float percentComplete) {
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
