using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onFinish;

    [SerializeField]
    private int seconds;

    private float timer = 0.0f;

    private bool paused = false;

    private void Update()
    {
        if(paused)
        {
            return;
        }

        // Every second
        timer += Time.deltaTime;
        if(HasTimeLeft && timer >= 1.0f)
        {
            seconds--;
            UpdateGameTime();
            timer = 0.0f;
        }

        if(!HasTimeLeft)
        {
            GameController.Instance.LoseGame("OUT OF TIME!");
            enabled = false;
        }
    }

    private void OnValidate()
    {
        UpdateGameTime();
    }

    public void Pause()
    {
        paused = !paused;
    }


    private string RemainingToString()
    {
        int mins = seconds / 60;
        int secs = seconds % 60;

        string time = mins.ToString("D2") + ":" + secs.ToString("D2");
        return time;
    }

    private void UpdateGameTime()
    {
        GetComponentInChildren<Text>().text = RemainingToString();
    }

    public bool HasTimeLeft
    {
        get
        {
            return seconds > 0;
        }
    }

}
