using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	public Connection[] connections;
	public List<Transform> availLeakLocations;
	public List<Leak> leaks;
	public Flood flood;

    // Use this for initialization
    void Start () {
        flood.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        foreach (Leak leak in leaks)
        {
            if (!leak.IsFixed())
            {
                flood.ChangeWaterValue(leak.waterPerSec * Time.deltaTime);
            }
        }
	}

	public void CleanUpLeaks() {
		for (int i = leaks.Count - 1; i >= 0; i--) {
			Leak leak = leaks[i];
			if (leak.IsFixed()) {
				availLeakLocations.Add(leak.transform.parent);
				leaks.RemoveAt(i);
				Destroy(leak.gameObject);
			}
		}
	}
}
