using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        gameState.StartRound();
        if (this == null) { return; }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPause()
    {
        if (gameState.isPaused)
        {
            gameState.isPaused = false;
			Time.timeScale = 1;
			SceneManager.UnloadSceneAsync("pause");
        }
        else
        {
			gameState.isPaused = true;
			Time.timeScale = 0;
			SceneManager.LoadSceneAsync("pause", LoadSceneMode.Additive);
        }
    }

    public void OnEndMission()
    {
        SceneManager.LoadSceneAsync("mission-debrief");
        SceneManager.UnloadSceneAsync("sean-sandbox");
    }

    public void OnEndMissionDebrief()
    {
        Debug.Log("End Mission Debrief Raised!");
        SceneManager.UnloadSceneAsync("mission-debrief");
        SceneManager.LoadSceneAsync("sean-sandbox");
        gameState.StartRound();
    }
}
