using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection {
	public Room roomA;
	public Room roomB;
	public Door door;

	public Room GetConnectedRoom(Room caller) {
		if (caller == roomA)  {
			return roomB;
		} else {
			return roomA;
		}
	}
}
