using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public GameObject subPrefab;
    public GameObject playerPrefab;
	public GameState gameState;
	public GameObject mapDisplayPrefab;
    private GameObject sub;
	private SubManager subManager;
	public Stage stage;
    private GameObject player;
	private GameObject mapDisplay;
	public Transform world;
	public Transform ui;
	private float missionStartTime;

	// Use this for initialization
	void Start () {
		sub = Instantiate(subPrefab, Vector3.zero, Quaternion.identity, world);
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, world);
		mapDisplay = Instantiate(mapDisplayPrefab, Vector3.zero, Quaternion.identity);
		mapDisplay.transform.SetParent(ui, false);

		stage.Build();

		subManager = sub.GetComponentInChildren<SubManager>();
		subManager.stage = stage;
		subManager.position = new SubPosition();
		subManager.position.row = stage.startRow;
		subManager.position.nextRow = stage.startRow;
		subManager.gameState = gameState;

		AddCargoToSub();

		mapDisplay.GetComponent<MapController>().subManager = subManager;
		sub.GetComponentInChildren<Parascope>().map = mapDisplay.GetComponent<MapController>();
		gameState.isInMission = true;
		missionStartTime = Time.time;
	}

	public void AddCargoToSub()
	{
		foreach (CargoCount cc in stage.cargoCounts)
		{
			for (int i = 0; i < cc.count; i++)
			{
				subManager.AddCargo(Instantiate(cc.cargoPrefab, Vector3.zero, Quaternion.identity));
			}
		}
	}

	public void Update()
	{
		gameState.missionTime += Time.deltaTime;
	}

	public void OnEndMission() {
		gameState.isInMission = false;
		gameState.coinMultiplier = stage.GetMultiplier(gameState.missionTime);
		foreach (Cargo cargo in sub.GetComponentInChildren<SubManager>().cargo)
        {
            if (cargo != null)
            {
                gameState.cargoToCount.Add(cargo);
            }
        }
	}
}
