using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {
	public float defaultRespawnTime;
	private HashSet<Respawn> respawns;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Respawn respawn in respawns) {
			if (Time.time > respawn.respawnTime) {
				Respawn(respawn.character);
				respawns.Remove(respawn);
			}
		}
	}

	public void StartRespawn(Character character) {
		Respawn respawn = new Respawn();
		respawn.character = character;
		respawn.respawnTime = Time.time + GetRespawnTime(character);
	}

	private void Respawn(Character character) {
		character.Revive();
	}

	float GetRespawnTime(Character character) {
		return defaultRespawnTime;
	}
}
