using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static PotionCraftManager;

public class ItemManager : MonoBehaviour
{
    public TextAsset itemData;

    private void Awake()
    {
        ReadJSON();
    }

    [System.Serializable]
    public class ItemList
    {
        public List<Item.ItemData> item;
    }
    
    public ItemList itemList;
    void ReadJSON()
    {
        Debug.Log("Item List");
        itemList = JsonUtility.FromJson<ItemList>(itemData.text);
    }
}
