using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;

    private void Start()
    {
        OnValidate();
    }

    public void Highlight()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_Highlight", 1.0f);
    }

    public void StopHighlight()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_Highlight", 0.0f);
    }

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.ItemSprite;
    }

    public ItemData Data
    {
        get { return itemData; }
        set { 
            itemData = value;
            OnValidate();
        }
    }
}
