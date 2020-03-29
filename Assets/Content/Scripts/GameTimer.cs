﻿using System.Collections;
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

    private void Update()
    {
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
            onFinish.Invoke();
            enabled = false;
        }
    }

    private void OnValidate()
    {
        UpdateGameTime();
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
