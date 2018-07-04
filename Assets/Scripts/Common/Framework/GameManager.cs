using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    private bool endMission;

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
        if (endMission) {
            SceneManager.LoadSceneAsync("mission-debrief");
            SceneManager.UnloadSceneAsync("sean-sandbox");
            endMission = false;
        }
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
        // Wait a frame before ending
        endMission = true;
    }

    public void OnEndMissionDebrief()
    {
        Debug.Log("Calling debrief end");
        SceneManager.UnloadSceneAsync("mission-debrief");
        SceneManager.LoadSceneAsync("sean-sandbox");
        gameState.StartRound();
    }
}
