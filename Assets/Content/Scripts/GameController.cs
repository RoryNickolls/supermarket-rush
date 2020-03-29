using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameTimer gameTimer;
    private bool started = false;

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void StartGame()
    {
        gameTimer.enabled = true;
        started = true;
    }

    public void LoseGame()
    {

    }

    public bool HasStarted
    {
        get { return started; }
    }
}
