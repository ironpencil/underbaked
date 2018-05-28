using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpStation : MonoBehaviour {
	public int pumpsToDrain;
	public float pumpFreq;
	private float nextPumpTime;
	public Color doneColor;
	public Color waitColor;
	public Color readyColor;
	public Nozzle nozzle;
	public Pump pump;
	private SpriteRenderer pumpStationSprite;
	public int currentPumps;
	public enum State {
		READY, WAIT, DONE
	}
	public State state;

	// Use this for initialization
	void Start () {
		state = State.READY;
		pumpStationSprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.WAIT && Time.time > nextPumpTime) {
			SetPumpReady();
		}
	}

	public void Pump() {
		if (state == State.READY) {
			currentPumps++;
			if (currentPumps == pumpsToDrain) {
				SetPumpDone();
			} else {
				SetPumpWait();
			}
		} else if (state == State.DONE) {
			nozzle.Pump();
			ResetPump();
		}
	}

	public void ResetPump() {
		SetPumpReady();
		currentPumps = 0;
	}

	public void SetPumpReady() {
		state = State.READY;
		pumpStationSprite.color = readyColor;
	}

	public void SetPumpWait() {
		state = State.WAIT;
		pumpStationSprite.color = waitColor;
		nextPumpTime = Time.time + pumpFreq;
	}

	public void SetPumpDone() {
		state = State.DONE;
		pumpStationSprite.color = doneColor;
	}
}