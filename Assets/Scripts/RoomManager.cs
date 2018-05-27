using System.Collections;
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
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextLeakTime) {
            nextLeakTime = Time.time + Random.Range(leakFreqMin, leakFreqMax);
			SpringRandomLeak();
        }

		SpreadWater();
	}

	void SpreadWater() {
		foreach (Connection conn in connections) {
			if ((conn.roomA.IsFlooding() || conn.roomB.IsFlooding()) 
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
