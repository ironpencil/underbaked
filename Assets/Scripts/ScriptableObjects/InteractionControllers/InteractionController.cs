using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionController : ScriptableObject {

    public abstract void Interact(GameObject target, GameObject interactor, Interaction interaction);
}
