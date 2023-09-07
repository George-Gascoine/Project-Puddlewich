using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static Inventory;
using static Item;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public Item.ItemData item;
        public int count;
        public int maxAllowed;
        public bool mouseOver = false;

        public Sprite icon;
        public Slot()
        {
            maxAllowed = 99;
        }

        public bool CanAddItem()
        {
            if (count < maxAllowed) 
            {
                Debug.Log(count);
                return true;
            }
                return false;
        }

        public void AddItem(Item.ItemData item)
        {
            this.item = item;
            this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.sprite);
            count += 1;
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        Debug.Log("here");
        slots.Clear();
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Item.ItemData item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item != null)
            {
                if (slot.item.id == item.id && slot.CanAddItem())
                {
                    slot.count += 1;
                    return;
                }
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.item == null && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void AddToSlot(Item.ItemData item, int id)
    {
        slots[id].AddItem(item);
    }

    public void Remove(int slotID)
    {
        if (slots[slotID].count != 0)
        {
            slots[slotID].count--;
            if (slots[slotID].count == 0)
            {
                slots[slotID].item = null;
            }
        }
    }
    public void RemoveAll(int slotID)
    {
        slots[slotID].count = 0;
        slots[slotID].item = null;
    }

    public bool CheckItem(Item.ItemData checkType, int checkAmount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == checkType && slot.count == checkAmount)
            {
                return true;
            }
        }
        return false;
    }
}
