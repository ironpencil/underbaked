﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
	public List<Connection> connections;
	public List<Room> rooms;
	public float leakFreqMin;
	public float leakFreqMax;
	private float nextLeakTime;
	public GameObject leakPrefab;

	// Use this for initialization
	void Start () {
		nextLeakTime = DetermineNextLeakTime();
	}
	
	// Update is called once per frame
	void Update () {
		CleanUpLeaks();

		if (Time.time > nextLeakTime) {
            nextLeakTime = DetermineNextLeakTime();
			SpringRandomLeak();
        }

		SpreadWater();
		CheckForExposures();
	}

	float DetermineNextLeakTime() {
		return Time.time + Random.Range(leakFreqMin, leakFreqMax);
	}

	void CleanUpLeaks() {
		foreach (Room room in rooms) {
			room.CleanUpLeaks();
		}
	}

	void CheckForExposures() {
		foreach (Room room in rooms) {
			room.CheckForExposures();
		}
	}

	void SpreadWater() {
		foreach(Room room in rooms) {
			if (room.IsLeaking()) {
				room.Flood();
			}
		}

		foreach (Connection conn in connections) {
			if ((conn.roomA.IsFlooded() || conn.roomB.IsFlooded()) 
			&& conn.door.IsOpen()) {
				conn.roomA.Flood();
				conn.roomB.Flood();
			}
		}
	}

	private void SpringRandomLeak() {
		Room room = GetRandomRoom();
		AddRandomLeak(room);
	}

	private Room GetRandomRoom() {
		int roomIdx = Random.Range(0, rooms.Count);
		return rooms[roomIdx];
	}

	private void AddRandomLeak(Room room) {
		if (room.availLeakLocations.Count > 0) {
			int leakLocIdx = Random.Range(0, room.availLeakLocations.Count);
			Transform location = room.availLeakLocations[leakLocIdx];
			room.availLeakLocations.RemoveAt(leakLocIdx);
			room.leaks.Add(Instantiate(leakPrefab, location));
			room.Flood();
		}
	}
}
