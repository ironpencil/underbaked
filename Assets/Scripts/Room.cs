using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	public Connection[] connections;
	public List<Transform> availLeakLocations;
	public List<Leak> leaks;
	public List<Vulnerability> vulnerables;

	public GameObject flood;
    SpriteRenderer floodSprite;

    public float waterValue = 0.0f;

    public int maxWaterVolume = 100;
    public int floodThreshold = 50;

    [SerializeField]
    int floodLevel = 0;
    public int FloodLevel {
        get { return floodLevel; }
        private set { floodLevel = value; }
    }

    // Use this for initialization
    void Start () {
        flood.SetActive(true);
        floodSprite = flood.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        foreach (Leak leak in leaks)
        {
            if (!leak.IsFixed())
            {
                ChangeWaterValue(leak.waterPerSec * Time.deltaTime);
            }
        }
	}

    void LateUpdate()
    {
        SetFloodLevel();
        DisplayFlooding();
    }

    private void DisplayFlooding()
    {
        Color newColor = floodSprite.color;
        newColor.a = waterValue / maxWaterVolume;
        floodSprite.color = newColor;

        //if (FloodLevel == 1)
        //{
        //    flood.SetActive(true);
        //} else
        //{
        //    flood.SetActive(false);
        //}
    }

    private void SetFloodLevel()
    {
        if (Mathf.RoundToInt(waterValue) >= floodThreshold)
        {
            FloodLevel = 1;
        }
        else
        {
            FloodLevel = 0;
        }
    }

    public void ChangeWaterValue(float waterValue)
    {
        this.waterValue = Mathf.Clamp(this.waterValue + waterValue, 0, maxWaterVolume);
    }

    public void OnTriggerEnter2D(Collider2D other) {
		Carrier carrier = other.GetComponent<Carrier>();
		
		if (carrier != null && carrier.heldObject != null) {
			AddVulnerable(carrier.heldObject.GetComponent<Vulnerability>());
		}
		AddVulnerable(other.GetComponent<Vulnerability>());
	}

	public void OnTriggerExit2D(Collider2D other) {
		Carrier carrier = other.GetComponent<Carrier>();
		
		if (carrier != null && carrier.heldObject != null) {
			RemoveVulnerable(carrier.heldObject.GetComponent<Vulnerability>());
		}
		RemoveVulnerable(other.GetComponent<Vulnerability>());
	}

	private void RemoveVulnerable(Vulnerability vulnerable) {
		if (vulnerable != null) {
			if (vulnerables.Contains(vulnerable)) {
				Debug.Log("Removing Vul: " + vulnerable.transform.name);
				vulnerables.Remove(vulnerable);
			}
		}
	}

	private void AddVulnerable(Vulnerability vulnerable) {
		if (vulnerable != null) {
			if (!vulnerables.Contains(vulnerable)) {
				Debug.Log("Adding Vul: " + vulnerable.transform.name);
				vulnerables.Add(vulnerable);
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

	public void CheckForExposures() {
		for (int i = vulnerables.Count - 1; i >= 0; i--) {
			Vulnerability vulnerable = vulnerables[i];
			if (FloodLevel > 0) {
				vulnerable.Expose(flood.GetComponent<Flood>().hazardType);
			}
		}
	}
}
