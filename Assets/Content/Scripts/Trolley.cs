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

    public void AddItem(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 3f), 0f);
    }
}
