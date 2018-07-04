using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class MissionDebriefManager : MonoBehaviour {
	public Text timeText;
	public Text coinText;
	public GameState state;
	public GameEvent endMissionDebrief;
	public MissionDebriefConfig config;
	public int displayValue;
	private int beforeMissionCoins;
	private int missionCoinValue;
	private int missionCoinValueWithMulti;
	private bool buttonPressed;

	// Use this for initialization
	void Start () {
		timeText.text = string.Format("{0}:{1:00}", (int)state.missionTime / 60, (int)state.missionTime % 60);
		UpdateState();
		StartCoroutine(TallyCargo());
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Player player in ReInput.players.Players)
		{
			if (player.GetAnyButtonDown())
			{
				buttonPressed = true;
			}
		}
	}

	void UpdateState() {
		beforeMissionCoins = state.coins;
		missionCoinValue = 0;
		missionCoinValueWithMulti = 0;
		foreach (Cargo cargo in state.cargoToCount)
		{
			missionCoinValue += cargo.stats.value;
		}
		missionCoinValueWithMulti = Mathf.FloorToInt(state.coinMultiplier * missionCoinValue);
		state.coins += missionCoinValueWithMulti;
	}

	IEnumerator TallyCargo()
	{
		Debug.Log("Before Mission Coins: " + beforeMissionCoins);
		Debug.Log("Mission Coin Value: " + missionCoinValue);
		Debug.Log("Mission Coin Value w Multi: " + missionCoinValueWithMulti);

		int startValue = beforeMissionCoins;
		int endValue = beforeMissionCoins + missionCoinValue;
		float elapsed = 0;

		while (elapsed <= config.cargoCountLength && !buttonPressed) {
			elapsed += Time.deltaTime;
			float lerp = elapsed / config.cargoCountLength;
			displayValue = (int)Mathf.Lerp(startValue, endValue, lerp);
			coinText.text = displayValue.ToString();
			yield return null;
		}

		coinText.text = (beforeMissionCoins + missionCoinValue).ToString();

		state.cargoToCount.Clear();

		while (!buttonPressed) yield return null;
		buttonPressed = false;

		startValue = endValue;
		endValue = beforeMissionCoins + missionCoinValueWithMulti;
		elapsed = 0;

		while (elapsed <= config.cargoCountLength && !buttonPressed) {
			elapsed += Time.deltaTime;
			float lerp = elapsed / config.cargoCountLength;
			displayValue = (int)Mathf.Lerp(startValue, endValue, lerp);
			coinText.text = displayValue.ToString();
			yield return null;
		}
		
		coinText.text = (beforeMissionCoins + missionCoinValueWithMulti).ToString();

		while (!buttonPressed) yield return null;
		buttonPressed = false;

		endMissionDebrief.Raise();
	}
}
