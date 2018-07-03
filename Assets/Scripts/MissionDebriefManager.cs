using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class MissionDebriefManager : MonoBehaviour {
	public Text coinText;
	public GameState state;
	public GameEvent endMissionDebrief;

	// Use this for initialization
	void Start () {
		coinText.text = state.coins.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Player player in ReInput.players.Players)
		{
			if (player.GetAnyButtonDown())
			{
				Debug.Log("Button down!");
				endMissionDebrief.Raise();
			}
		}
	}
}
