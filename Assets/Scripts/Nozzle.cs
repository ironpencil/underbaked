using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nozzle : MonoBehaviour {
	public Room room;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pump() {
		//room.StartDraining();
	}

	// Did not implement OnTriggerExit2D because a nozzle
	// should always be in some room
	public void OnTriggerEnter2D(Collider2D other) {
		Room otherRoom = other.GetComponent<Room>();
		
		if (otherRoom != null) {
			room = otherRoom;
		}
	}
}
