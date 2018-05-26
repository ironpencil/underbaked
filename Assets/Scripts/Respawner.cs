using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {
	public float defaultRespawnTime;
	private HashSet<Respawn> respawns;
	public Transform topLeftBound;
	public Transform bottomRightBound;

	// Use this for initialization
	void Start () {
		respawns = new HashSet<Respawn>();
	}
	
	// Update is called once per frame
	void Update () {
		List<Respawn> respawned = new List<Respawn>();
		foreach (Respawn respawn in respawns) {
			//Debug.Log("Time.time: " + Time.time + ", respawn.respawnTime: " + respawn.respawnTime);
			if (Time.time > respawn.respawnTime) {
				Respawn(respawn.character);
				respawned.Add(respawn);
			}
		}
		foreach (Respawn respawn in respawned) {
			respawns.Remove(respawn);
		}
	}

	public void StartRespawn(Character character) {
		Respawn respawn = new Respawn();
		respawn.character = character;
		respawn.respawnTime = Time.time + GetRespawnTime(character);
		respawns.Add(respawn);
	}

	private void Respawn(Character character) {
		float x = Random.Range(topLeftBound.position.x, bottomRightBound.position.x);
		float y = Random.Range(bottomRightBound.position.y, topLeftBound.position.y);

		character.Revive(new Vector2(x, y));
	}

	float GetRespawnTime(Character character) {
		return defaultRespawnTime;
	}
}
