using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	private List<Transform> leakLocations;
	private List<Leak> leaks;
	private FloodBehavior flood;
	public GameObject leakPrefab;

    // Use this for initialization
    void Start () {
		foreach(LeakLoc leakloc in GetComponentsInChildren<LeakLoc>()) {
			leakLocations.Add(leakloc.transform);
		}
		flood = GetComponent<FloodBehavior>();
		if (flood != null) flood.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        foreach (Leak leak in leaks)
        {
            if (!leak.IsFixed() && flood != null)
            {
                flood.ChangeWaterValue(leak.waterPerSec * Time.deltaTime);
            }
        }
	}

	public void CleanUpLeaks() {
		for (int i = leaks.Count - 1; i >= 0; i--) {
			Leak leak = leaks[i];
			if (leak.IsFixed()) {
				leakLocations.Add(leak.transform.parent);
				leaks.RemoveAt(i);
				Destroy(leak.gameObject);
			}
		}
	}

	public List<Transform> GetLeakLocations() {
		return leakLocations;
	}

	public void AddLeak(Transform location) {
		if (leakLocations.Contains(location)) {
			leakLocations.Remove(location);
		}
		leaks.Add(Instantiate(leakPrefab, location).GetComponent<Leak>());
	}

	public float GetWaterValue() {
		if (flood != null) {
			return flood.waterValue;
		} else {
			return 0;
		}
	}

	public void ChangeWaterValue(float value) {
		if (flood != null) {
			flood.ChangeWaterValue(value);
		}
	}
}
