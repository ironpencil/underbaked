using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpStation : InteractableBehavior {
	public int pumpsToDrain;
	public float waitLength;
	private float nextPumpTime;
	public float length;
	public float amountPerDrain;
	public Color doneColor;
	public Color waitColor;
	public Color readyColor;
	public Color pumpinColor;
	public Nozzle nozzle;
	public Pump pump;
	private SpriteRenderer pumpStationSprite;
	public int currentPumps;
	public enum State {
		READY, WAIT, DONE, PUMPIN
	}
	public State state;

	// Use this for initialization
	void Start () {
		pumpStationSprite = gameObject.GetComponent<SpriteRenderer>();
		ResetPump();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.WAIT && Time.time > nextPumpTime) {
			if (currentPumps >= pumpsToDrain) {
				SetPumpDone();
			} else {
				SetPumpReady();
			}
		} else if (state == State.PUMPIN) {
			if (!nozzle.IsPumping()) {
				ResetPump();
			}
		}
	}

	public virtual void OnInteract(GameObject interactor, Interaction interaction)
    {
        Push();
    }

	public void Push() {
		if (state == State.READY) {
			currentPumps++;
			SetPumpWait();
		} else if (state == State.DONE) {
			nozzle.StartPumping(length, amountPerDrain);
			SetPumpPumpin();
		}
	}

	public void ResetPump() {
        currentPumps = 0;
        SetPumpWait();
	}

	public void SetPumpReady() {
		state = State.READY;
		pumpStationSprite.color = readyColor;
	}

	public void SetPumpWait() {
		state = State.WAIT;
		pumpStationSprite.color = waitColor;
		nextPumpTime = Time.time + waitLength;
	}

	public void SetPumpDone() {
		state = State.DONE;
		pumpStationSprite.color = doneColor;
	}

	public void SetPumpPumpin() {
		state = State.PUMPIN;
		pumpStationSprite.color = pumpinColor;
	}
}