using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : MonoBehaviour {
	public Status status;
	public enum Status {
		BROKEN, FIXED
	}
	// Use this for initialization
	void Start () {
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
}
