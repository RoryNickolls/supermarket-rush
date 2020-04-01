using UnityEngine;
using System.Collections.Generic;

public class Trolley : MonoBehaviour
{

    private Dictionary<ItemData, int> items;
    private TrailRenderer[] trails;

    private void Awake()
    {
        items = new Dictionary<ItemData, int>();
    }

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

    public void AddItem(Item item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 3f), 0f);

        if(items.ContainsKey(item.Data))
        {
            items[item.Data] += 1;
        }
        else
        {
            items.Add(item.Data, 1);
        }
    }

    public Dictionary<ItemData, int> Items
    {
        get { return items; }
    }
}
