using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public Collectable.ItemType type;
        public int count;
        public int maxAllowed;

        public Sprite icon;
        public Slot()
        {
            type = Collectable.ItemType.NONE;
            count = 0;
            maxAllowed = 99;
        }

        public bool CanAddItem()
        {
            if (count < maxAllowed) 
            {
                return true;
            }
                return false;
        }

        public void AddItem(Collectable item)
        {
            this.type = item.type;
            this.icon = item.icon;
            count += 1;
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Collectable item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.type == item.type && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.type == Collectable.ItemType.NONE && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }
    }
    public void Remove(int slotID)
    {
        if (slots[slotID].count != 0)
        {
            slots[slotID].count--;
            if (slots[slotID].count == 0)
            {
                slots[slotID].type = Collectable.ItemType.NONE;
            }
        }
    }

    public bool CheckItem(Collectable.ItemType checkType, int checkAmount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.type == checkType && slot.count == checkAmount)
            {
                return true;
            }
        }
        return false;
    }
}
