using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingList : MonoBehaviour
{
    [SerializeField]
    private GameObject shoppingListItemPrefab;

    private Dictionary<ItemData, int> shoppingList;

    private void Awake()
    {
        shoppingList = new Dictionary<ItemData, int>();
    }

    private void Start()
    {
    }

    public void Create(int count)
    {
        // Add items to shopping list
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

        UpdateUI();
    }

    private void UpdateUI()
    {
        float yOffset = 0;
        foreach(ItemData data in shoppingList.Keys)
        {
            GameObject item = Instantiate(shoppingListItemPrefab, transform);
            item.GetComponent<RectTransform>().localPosition = new Vector3(0, -yOffset, 0);

            item.GetComponentInChildren<Text>().text = shoppingList[data].ToString() + " x";
            item.GetComponentInChildren<Image>().sprite = data.ItemSprite;

            yOffset += shoppingListItemPrefab.GetComponent<RectTransform>().rect.height;
        }
    }
}
