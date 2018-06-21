using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : ProgressiveInteractableBehavior {
    public int waterPerSec = 10;
	public Status status;
	public enum Status {
		BROKEN, FIXED
	}
	// Use this for initialization
	public override void Start () {
		ic.Subscribe(this);
		status = Status.BROKEN;
	}
	
	// Update is called once per frame
	void Update () {
		
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
