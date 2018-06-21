using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : ProgressiveInteractableBehavior, Consumer, Producer {
	public SpriteRenderer workSprite;
	public Color startColor;
    public Color finishColor;
    public Vector2 startScale;
    public Vector2 finishScale;
	public Carryable product;

	public override void OnBegin(GameObject interactor, Interaction interaction)
    {
        finishColor = product.GetComponent<SpriteRenderer>().color;
        finishScale = product.transform.localScale;
        workSprite.enabled = true;
    }

    public void OnConsume(Carrier carrier, Consumable consumable, Interaction interaction)
    {
        startColor = consumable.GetComponent<SpriteRenderer>().color;
		startScale = consumable.transform.localScale;
    }

    public override void OnUpdate(GameObject interactor, Interaction interaction, Progression progression)
    {
        workSprite.color = GetLerpColor(startColor, finishColor, GetPercentComplete(progression));
        workSprite.transform.localScale = GetLerpScale(startScale, finishScale, GetPercentComplete(progression));
    }

	public override void OnFinish(GameObject interactor, Interaction interaction)
    {
        workSprite.enabled = false;
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

    float GetPercentComplete(Progression progression) {
        return progression.elapsedTime / progression.length;
    }

    public void OnProduce(GameObject interactor, Carryable carryable, Interaction interaction)
    {
        interactor.GetComponent<Carrier>().PickUp(Instantiate(product, interactor.GetComponent<Carrier>().transform));
    }
}
