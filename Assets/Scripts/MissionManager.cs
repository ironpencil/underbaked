using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public GameObject subPrefab;
    public GameObject playerPrefab;
	public GameState gameState;
	public GameObject mapDisplayPrefab;
    private GameObject sub;
    private GameObject player;
	private GameObject mapDisplay;
	public Transform world;
	public Transform ui;

	// Use this for initialization
	void Start () {
		sub = Instantiate(subPrefab, Vector3.zero, Quaternion.identity, world);
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, world);
		mapDisplay = Instantiate(mapDisplayPrefab, Vector3.zero, Quaternion.identity);
		mapDisplay.transform.SetParent(ui, false);
		mapDisplay.GetComponent<MapController>().ship = sub.GetComponentInChildren<ShipManager>();
		sub.GetComponentInChildren<Parascope>().map = mapDisplay.GetComponent<MapController>();
		gameState.isInMission = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnEndMission() {
		foreach (Loot loot in sub.GetComponentInChildren<ShipManager>().loot)
        {
            if (loot != null)
            {
                gameState.lootToCount.Add(loot);
				gameState.isInMission = false;
            }
        }
	}
}
