using UnityEngine;
using System.Collections;

public class Trolley : MonoBehaviour
{
    private TrailRenderer[] trails;


    private void Start()
    {
        trails = GetComponentsInChildren<TrailRenderer>();
    }

    public void StartDrifting()
    {
        foreach(TrailRenderer trail in trails)
        {
            trail.startColor = Color.yellow;
            trail.endColor = new Color(1.0f, 0.7f, 0f);
        }
    }

    public void StopDrifting()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.startColor = Color.white;
            trail.endColor = new Color(1, 1, 1, 0);
        }
    }
}
