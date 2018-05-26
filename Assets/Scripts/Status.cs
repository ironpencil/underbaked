using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status {
	Character affectedCharacter;
	public Status(Character character) {
		this.affectedCharacter = character;
	}

	public abstract void Update(float delta);
}
