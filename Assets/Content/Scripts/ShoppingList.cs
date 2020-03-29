using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{

    private Dictionary<ItemData, int> shoppingList;

    private void Awake()
    {
        shoppingList = new Dictionary<ItemData, int>();
    }

    public void Create(int count)
    {
        Item[] sceneItems = FindObjectsOfType<Item>();
        for(int i = 0; i < count && i < sceneItems.Length; i++)
        {
            Item item = sceneItems[Random.Range(0, sceneItems.Length)];
            if(shoppingList.ContainsKey(item.Data))
            {
                shoppingList[item.Data] += 1;
            }
            else
            {
                shoppingList.Add(item.Data, 1);
            }
        }
    }
}
