using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{
    public float effectiveSpeed;

    public Slow(Character character) : base(character)
    {
        
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
