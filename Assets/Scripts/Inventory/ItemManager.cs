using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Collectable[] collectableItems;
    private Dictionary<Collectable.ItemType, Collectable> collectableItemDict = new();

    private void Awake()
    {
        foreach(Collectable item in collectableItems)
        {
            AddItem(item);
        }    
    }

    private void AddItem(Collectable item)
    {
        if (!collectableItemDict.ContainsKey(item.type))
        {
            collectableItemDict.Add(item.type, item); 
        }
    }

    public Collectable GetItemByType(Collectable.ItemType type)
    {
        if (collectableItemDict.ContainsKey(type)) 
        { 
            return collectableItemDict[type]; 
        } 
        return null; 
    }
}
