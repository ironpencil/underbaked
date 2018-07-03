using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game State")]
public class GameState : ScriptableObject {
	public int coins;
	public bool isPaused;

	public void StartRound()
	{
		isPaused = false;
	}
	public void ResetGame()
	{
		coins = 0;
	}
}
