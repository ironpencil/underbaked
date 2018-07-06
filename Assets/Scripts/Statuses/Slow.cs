using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{
    private float effectiveSpeed;

    public Slow(Character character, float effectiveSpeed) : base(character)
    {
        this.effectiveSpeed = effectiveSpeed;
    }

    public override void Begin()
    {
        affectedCharacter.currentSpeed = effectiveSpeed * affectedCharacter.defaultSpeed;
    }

    public override void End()
    {
        affectedCharacter.currentSpeed = affectedCharacter.defaultSpeed;
    }

    public override void Update(float delta)
    {
        
    }
}
