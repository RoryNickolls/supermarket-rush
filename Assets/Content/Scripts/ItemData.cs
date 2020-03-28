using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item", order = 0)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private Sprite itemSprite;

    [SerializeField]
    private ItemGroup itemGroup;

    public Sprite ItemSprite
    {
        get { return itemSprite; }
    }

    public ItemGroup Group
    {
        get { return itemGroup; }
    }
}
public enum ItemGroup
{
    Vegetable,
    Fruit,
    MeatAndTrash,
    Sweet
}
