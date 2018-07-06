using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drowning : StatusEffect
{
	public float drownLifetime;
	private float drownLength;

    public Drowning(Character character) : base(character)
    {
        this.drownLifetime = character.breathLength;
    }

    public override void Begin()
    {
        
    }

    public override void Update(float delta)
    {
        drownLength += Time.deltaTime;
		if (drownLength > drownLifetime && affectedCharacter != null) {
			affectedCharacter.Die();
		}
    }

    public override void End()
    {
        
    }
}
