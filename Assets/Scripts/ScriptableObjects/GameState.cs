using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game State")]
public class GameState : ScriptableObject {
	public List<Cargo> cargoToCount;
	public float coinMultiplier;
	public float missionTime;
	public int coins;
	public bool isPaused;
	public bool isInMission;

	public void StartRound()
	{
		cargoToCount = new List<Cargo>();
		missionTime = 0;
		isPaused = false;
	}
	public void ResetGame()
	{
		coins = 0;
	}
}
