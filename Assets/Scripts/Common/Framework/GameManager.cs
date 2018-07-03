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
			SceneManager.LoadScene("pause", LoadSceneMode.Additive);
        }
    }
}
