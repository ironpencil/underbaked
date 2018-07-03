using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    [ConnectionList]
	public List<MapConnection> connections;

    public bool springLeaks = true;
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

        if (springLeaks)
        {
            if (Time.time > nextLeakTime)
            {
                nextLeakTime = DetermineNextLeakTime();
                SpringRandomLeak();
            }
        }
		
		SpreadWater();
	}

	float DetermineNextLeakTime() {
		return Time.time + Random.Range(leakFreqMin, leakFreqMax);
	}

	void CleanUpLeaks() {
		foreach (Room room in rooms) {
			room.CleanUpLeaks();
		}
	}

	void SpreadWater() {

        Dictionary<Room, float> waterValues = new Dictionary<Room, float>();

        foreach (MapConnection conn in connections)
        {
            if (conn.door.IsOpen())
            {
                float difference = conn.from.flood.waterValue - conn.to.flood.waterValue;
                if (difference != 0)
                {
                    float delta = difference * 0.5f;
                    float fromDelta = delta * -1 * Time.deltaTime;
                    float toDelta = delta * Time.deltaTime;

                    float fromValue = 0;
                    waterValues.TryGetValue(conn.from, out fromValue);
                    float toValue = 0;
                    waterValues.TryGetValue(conn.to, out toValue);

                    waterValues[conn.from] = fromValue + fromDelta;
                    waterValues[conn.to] = toValue + toDelta;
                }
            }
        }

        //we should now have a list of all rooms and how much they should change by
        //so change the room values

        foreach (var kvp in waterValues)
        {
            kvp.Key.flood.ChangeWaterValue(kvp.Value);
        }
	}

    [ContextMenu("Spring Random Leak")]
	public void SpringRandomLeak() {
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
			room.leaks.Add(Instantiate(leakPrefab, location).GetComponent<Leak>());
		}
	}
}
