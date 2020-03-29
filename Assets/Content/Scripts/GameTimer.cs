using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    [SerializeField]
    private int seconds;

    private float timer = 0.0f;


    private void Update()
    {
        // Every second
        timer += Time.deltaTime;
        if(timer >= 1.0f)
        {
            seconds--;
            UpdateGameTime();
            timer = 0.0f;
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

}
