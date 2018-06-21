using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : ProgressiveInteractableImpl {
    public int waterPerSec = 10;
	public Status status = Status.BROKEN;
	public enum Status {
		BROKEN, FIXED
	}
	public bool IsFixed() {
		return status == Status.FIXED;
	}
	public void FixLeak() {
		status = Status.FIXED;
	}
	public override void OnFinish(GameObject interactor, Interaction interaction)
    {
        FixLeak();
    }
}
