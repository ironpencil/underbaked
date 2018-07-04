using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class MissionDebriefManager : MonoBehaviour {
	public Text coinText;
	public GameState state;
	public GameEvent endMissionDebrief;
	public MissionDebriefConfig config;
	public int displayValue;

	// Use this for initialization
	void Start () {
		displayValue = state.coins;
		UpdateState();
		StartCoroutine(TallyLoot());
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Player player in ReInput.players.Players)
		{
			Debug.Log("Checking for player " + player.id);
			if (player.GetAnyButtonDown())
			{
				Debug.Log("End mission debrief raised");
				endMissionDebrief.Raise();
			}
		}
	}

	void UpdateState() {
		foreach (Loot loot in state.lootToCount)
		{
			state.coins += loot.stats.value;
		}
	}

	IEnumerator TallyLoot()
	{
		foreach (Loot loot in state.lootToCount)
		{
			int startValue = displayValue;
			int endValue = displayValue + loot.stats.value;
			float elapsed = 0;
			while (elapsed <= config.lootCountLength) {
				elapsed += Time.deltaTime;
				float lerp = elapsed / config.lootCountLength;
				displayValue = (int)Mathf.Lerp(startValue, endValue, lerp);
				coinText.text = displayValue.ToString();
				yield return null;
			}
		}
		state.lootToCount.Clear();
	}
}
