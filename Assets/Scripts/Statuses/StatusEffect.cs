using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
	protected Character affectedCharacter;
	public StatusEffect(Character character) {
		this.affectedCharacter = character;
	}

	public abstract void Begin();
	public abstract void Update(float delta);
	public abstract void End();
}
