using UnityEngine;
using System.Collections;

public class Shelf : MonoBehaviour
{
    [SerializeField]
    private ItemGroup itemGroup;

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
            if (pos == transform)
                continue;

            Item item = ItemManager.Instance.GetRandomItem(itemGroup);
            item.transform.SetParent(pos);
            item.transform.localPosition = Vector3.zero;
        }
    }
}
