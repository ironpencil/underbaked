using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    [ConnectionList]
	public List<MapConnection> connections;
    
    List<Room> rooms;
    public float leakFreqMin = 10;
    public float leakFreqMax = 20;
	private float nextLeakTime;
	public GameObject leakPrefab;

    private void Awake()
    {
        rooms = connections.SelectMany(connection => new List<Room>() { connection.from, connection.to }).Distinct().ToList();
    }
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
		
		DrainWater();
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

		foreach (MapConnection conn in connections) {
			if ((conn.from.IsFlooded() || conn.to.IsFlooded()) 
			&& conn.door.IsOpen()) {
				conn.from.Flood();
				conn.to.Flood();
			}
		}
	}

	void DrainWater() {
		foreach (Connection conn in connections) {
			if ((conn.roomA.IsDraining() || conn.roomB.IsDraining()) 
			&& conn.door.IsOpen()) {
				conn.roomA.StartDraining();
				conn.roomB.StartDraining();
			}
		}

		foreach (Room room in rooms) {
			if (room.IsDraining()) {
				room.Drain();
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
