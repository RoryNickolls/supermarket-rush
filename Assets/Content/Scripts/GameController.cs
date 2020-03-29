using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameTimer gameTimer;
    private bool started = false;

    [SerializeField]
    private GameObject lose;

    [SerializeField]
    private AudioClip loseClip;

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void StartGame()
    {
        FindObjectOfType<ShoppingList>().Create(3);
        gameTimer.enabled = true;
        started = true;
    }

    public void LoseGame()
    {
        lose.SetActive(true);
        Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.5f);
        started = false;

        AudioManager.PlayOnce(loseClip);
    }

    public bool HasStarted
    {
        get { return started; }
    }
}
