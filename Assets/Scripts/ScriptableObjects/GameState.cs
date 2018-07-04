using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game State")]
public class GameState : ScriptableObject {
	public List<Loot> lootToCount;
	public int coins;
	public bool isPaused;
	public bool isInMission;

	public void StartRound()
	{
		lootToCount = new List<Loot>();
		isPaused = false;
	}
	public void ResetGame()
	{
		coins = 0;
	}
}
