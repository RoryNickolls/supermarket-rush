using UnityEngine;
using System.Collections;

public class Shelf : MonoBehaviour
{
    [SerializeField]
    private ItemGroup itemGroup;

    [SerializeField]
    private float spawnChance = 0.75f;

    private Transform[] itemPositions;

    private void Start()
    {
        itemPositions = GetComponentsInChildren<Transform>();

        SpawnItems();
    }

    private void SpawnItems()
    {
        foreach(Transform pos in itemPositions)
        {
            if (pos == transform || Random.Range(0.0f, 1.0f) > spawnChance)
                continue;

            Item item = ItemManager.Instance.GetRandomItem(itemGroup);
            item.transform.SetParent(pos);
            item.transform.localPosition = Vector3.zero;
        }
    }
}
