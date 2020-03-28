using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance;

    private Dictionary<ItemGroup, ItemData[]> groupedData;

    [SerializeField]
    private Item itemPrefab;

    [SerializeField]
    private ItemData[] allItems;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("ItemManager already exists!");
            Destroy(gameObject);
        }

        ProcessItems();
    }

    private void ProcessItems()
    {
        groupedData = new Dictionary<ItemGroup, ItemData[]>();
        foreach(ItemGroup group in Enum.GetValues(typeof(ItemGroup)))
        {
            ItemData[] groupItems = allItems.Where(data => data.Group == group).ToArray();
            groupedData.Add(group, groupItems);
        }
    }

    public Item GetRandomItem(ItemGroup group)
    {
        Item item = Instantiate(itemPrefab);

        ItemData[] items = groupedData[group];
        if (items.Length > 0)
        {
            item.Data = items[Random.Range(0, items.Length)];
        }

        return item;
    }
}
