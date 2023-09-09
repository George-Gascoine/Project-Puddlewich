using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static Crop;
using static Inventory;
using static Item;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot 
    {
        public int slotID;
        public Item.ItemData item;
        public int count;
    }

    public List<Slot> slots = new List<Slot>();

    public void CreateInventory(int size) 
    { 
      for (int i = 0; i < size; i++)
        {
            Slot slot = new Slot();
            slot.slotID = i;
            slot.item = new Item.ItemData();
            slot.count = 0;
            slots.Add(slot);
        }
    }

    public void AddItem(Item.ItemData item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == item)
            {
                slot.count++;
                return;
            }
            else if (slot.item.id == 0)
            {
                slot.item.name = item.name;
                slot.item.id = item.id;
                slot.item.type = item.type;
                slot.item.effect = item.effect;
                slot.item.description = item.description;
                slot.item.sprite = item.sprite;
                slot.item.cost = item.cost;
                slot.item.quality = item.quality;
                slot.count = 1;
                return;
            }
        }
    }

    public void AddToSlot(int id, ItemData data, int quantity)
    {
        slots[id].item = data;
        slots[id].count = quantity;
    }
    public void RemoveItem(int id)
    {
        slots[id].item = null;
    }
    public void RemoveAll(int id)
    {
        slots[id].count = 0;
        slots[id].item = new Item.ItemData();
    }
}
