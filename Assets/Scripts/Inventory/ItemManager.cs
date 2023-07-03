using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Collectable[] collectableItems;
    public Sprite[] collectableSprites;
    public float[] collectablePrices;
    private Dictionary<Collectable.ItemType, Collectable> collectableItemDict = new();
    private Dictionary<Collectable.ItemType, Sprite> collectableSpriteDict = new();
    private Dictionary<Collectable.ItemType, float> collectablePriceDict = new();

    private void Awake()
    {
        foreach(Collectable item in collectableItems)
        {
            AddItem(item);
            foreach (Sprite sprite in collectableSprites)
            {
                AddSprite(item, sprite);
            }
            foreach (float price in collectablePrices)
            {
                AddPrice(item, price);
            }
        } 
    }
    private void AddItem(Collectable item)
    {
        if (!collectableItemDict.ContainsKey(item.type))
        {
            collectableItemDict.Add(item.type, item);
        }
    }
    private void AddSprite(Collectable item, Sprite sprite)
    {
        if (!collectableSpriteDict.ContainsKey(item.type))
        {
            collectableSpriteDict.Add(item.type, sprite);
        }
    }
    private void AddPrice(Collectable item, float price)
    {
        if (!collectablePriceDict.ContainsKey(item.type))
        {
            collectablePriceDict.Add(item.type, price);
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
    public Sprite GetSpriteByType(Collectable.ItemType type)
    {
        if (collectableSpriteDict.ContainsKey(type))
        {
            return collectableSpriteDict[type];
        }
        return null;
    }

    public float GetPriceByType(Collectable.ItemType type)
    {
        if (collectablePriceDict.ContainsKey(type))
        {
            return collectablePriceDict[type];
        }
        return 0.0f;
    }
}
